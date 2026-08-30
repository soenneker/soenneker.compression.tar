using Microsoft.Extensions.Logging;
using SharpCompress.Archives;
using SharpCompress.Archives.Tar;
using Soenneker.Compression.Tar.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpCompress.Writers.Tar;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Compression.Tar;

/// <inheritdoc cref="ITarUtil"/>
public sealed class TarUtil : ITarUtil
{
    private readonly ILogger<TarUtil> _logger;

    public TarUtil(ILogger<TarUtil> logger)
    {
        _logger = logger;
    }

    public async ValueTask Extract(string filePath, string outputDir, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Extracting tar file: {FilePath} to {OutputFilePath} ...", filePath, outputDir);

        string rootPath = EnsureTrailingSeparator(Path.GetFullPath(outputDir));
        Directory.CreateDirectory(rootPath);

        var destinationPaths = new HashSet<string>(OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
        await using IWritableAsyncArchive<TarWriterOptions> archive = await TarArchive.OpenAsyncArchive(filePath, cancellationToken: cancellationToken).NoSync();

        await foreach (IArchiveEntry entry in archive.EntriesAsync.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            if (entry.IsDirectory || string.IsNullOrEmpty(entry.Key))
                continue;

            if (!string.IsNullOrEmpty(entry.LinkTarget))
                throw new InvalidDataException($"Archive entry is a link and cannot be extracted safely: {entry.Key}");

            string destinationPath = GetSafeDestinationPath(rootPath, entry.Key);
            if (!destinationPaths.Add(destinationPath))
                throw new InvalidDataException($"Multiple archive entries resolve to the same destination: {entry.Key}");

            string? parentDirectory = Path.GetDirectoryName(destinationPath);
            if (parentDirectory is not null)
                Directory.CreateDirectory(parentDirectory);

            await entry.WriteToFileAsync(destinationPath, null, cancellationToken).NoSync();
        }
    }

    private static string GetSafeDestinationPath(string rootPath, string entryPath)
    {
        string relativePath = entryPath.Replace('/', Path.DirectorySeparatorChar);
        string destinationPath = Path.GetFullPath(Path.Combine(rootPath, relativePath));
        StringComparison comparison = OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        if (!destinationPath.StartsWith(rootPath, comparison))
            throw new InvalidDataException($"Archive entry path escapes the destination directory: {entryPath}");

        return destinationPath;
    }

    private static string EnsureTrailingSeparator(string path)
    {
        if (Path.EndsInDirectorySeparator(path))
            return path;

        return path + Path.DirectorySeparatorChar;
    }
}
