[![](https://img.shields.io/nuget/v/soenneker.compression.tar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.compression.tar.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.compression.tar/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.compression.tar/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.compression.tar/actions/workflows/codeql.yml)

# Soenneker.Compression.Tar

A utility library dealing with Tar compression and decompression.

## Install

```bash
dotnet add package Soenneker.Compression.Tar
```

## Quick start

```csharp
using Soenneker.Compression.Tar.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddTarUtilAsSingleton();
```

Adds `ITarUtil` as a singleton service.

## What you get

- `ITarUtil` — A utility library dealing with Tar compression and decompression.
- `TarUtilRegistrar` — A utility library dealing with Tar compression and decompression.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ITarUtil.Extract(filePath, outputDir, cancellationToken)` | Extracts the contents of the specified archive file to the given output directory asynchronously. | A ValueTask that represents the asynchronous extraction operation. |
| `TarUtilRegistrar.AddTarUtilAsSingleton(services)` | Adds `ITarUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `TarUtilRegistrar.AddTarUtilAsScoped(services)` | Adds `ITarUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
