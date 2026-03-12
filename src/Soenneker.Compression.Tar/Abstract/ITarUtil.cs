using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.Abstract;

/// <summary>
/// A utility library dealing with Tar compression and decompression
/// </summary>
public interface ITarUtil
{
    /// <summary>
    /// Extracts the contents of the specified archive file to the given output directory asynchronously.
    /// </summary>
    /// <param name="filePath">The path to the archive file to extract. Cannot be null or empty.</param>
    /// <param name="outputDir">The directory where the extracted files will be placed. Must be a valid, writable directory path.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the extraction operation.</param>
    /// <returns>A ValueTask that represents the asynchronous extraction operation.</returns>
    ValueTask Extract(string filePath, string outputDir, CancellationToken cancellationToken = default);
}