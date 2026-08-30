using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Compression.Tar.Abstract;

/// <summary>
/// Extracts TAR archives to a caller-selected directory.
/// </summary>
public interface ITarUtil
{
    /// <summary>
    /// Extracts regular files from a TAR archive into the specified directory.
    /// </summary>
    /// <param name="filePath">The path to the archive file to extract. Cannot be null or empty.</param>
    /// <param name="outputDir">The destination directory. It is created when it does not exist.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the extraction operation.</param>
    /// <returns>A task representing the extraction operation.</returns>
    /// <remarks>Links and entries that escape or collide within the destination are rejected. Files written before a later failure are not removed.</remarks>
    ValueTask Extract(string filePath, string outputDir, CancellationToken cancellationToken = default);
}
