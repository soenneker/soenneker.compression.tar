[![](https://img.shields.io/nuget/v/soenneker.compression.tar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.compression.tar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar/actions/workflows/codeql.yml)

# Soenneker.Compression.Tar

Extracts TAR archives into a directory with path-containment and link checks.

## Install

```bash
dotnet add package Soenneker.Compression.Tar
```

## Registration

```csharp
using Soenneker.Compression.Tar.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTarUtilAsSingleton();
```

Use `AddTarUtilAsScoped()` instead when its lifetime should follow a dependency-injection scope.

## Usage

```csharp
using Soenneker.Compression.Tar.Abstract;

public sealed class ArchiveImporter(ITarUtil tarUtil)
{
    public ValueTask Extract(string archivePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        return tarUtil.Extract(archivePath, destinationPath, cancellationToken);
    }
}
```

`Extract` creates the destination directory if necessary and writes regular archive entries beneath it. It rejects symbolic and hard links, paths that resolve outside the destination, and multiple entries that resolve to the same output path.

## Practical notes

- This package extracts TAR archives; it does not create them.
- The destination is caller-owned. If extraction fails or is cancelled, files already written are left in place.
- Existing-file behavior is controlled by SharpCompress. Use an empty destination when replacement semantics matter.
- Path checks do not protect against oversized or highly compressible archives. Apply application-specific file-count, size, and storage limits before accepting untrusted content.
