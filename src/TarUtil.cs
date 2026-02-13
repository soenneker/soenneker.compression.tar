using Microsoft.Extensions.Logging;
using SharpCompress.Archives;
using SharpCompress.Archives.Tar;
using Soenneker.Compression.Tar.Abstract;
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

        IWritableAsyncArchive<TarWriterOptions> archive = await TarArchive.OpenAsyncArchive(filePath, cancellationToken: cancellationToken).NoSync();

        await foreach (IArchiveEntry entry in archive.EntriesAsync.WithCancellation(cancellationToken))
        {
            if (!entry.IsDirectory)
            {
                await entry.WriteToDirectoryAsync(outputDir, cancellationToken).NoSync();
            }
        }
    }
}