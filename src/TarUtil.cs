﻿using Microsoft.Extensions.Logging;
using SharpCompress.Archives;
using SharpCompress.Archives.Tar;
using SharpCompress.Common;
using Soenneker.Compression.Tar.Abstract;
using System.Threading;

namespace Soenneker.Compression.Tar;

/// <inheritdoc cref="ITarUtil"/>
public sealed class TarUtil : ITarUtil
{
    private readonly ILogger<TarUtil> _logger;

    public TarUtil(ILogger<TarUtil> logger)
    {
        _logger = logger;
    }

    public void Extract(string filePath, string outputDir, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Extracting tar file: {FilePath} to {OutputFilePath} ...", filePath, outputDir);

        using TarArchive archive = TarArchive.Open(filePath);

        foreach (TarArchiveEntry entry in archive.Entries)
        {
            // Check for cancellation before processing each entry
            cancellationToken.ThrowIfCancellationRequested();

            if (!entry.IsDirectory)
            {
                entry.WriteToDirectory(outputDir, new ExtractionOptions
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }
    }
}