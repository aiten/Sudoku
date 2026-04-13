# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build entire solution
dotnet build Sudoku.sln

# Run all tests
dotnet test Sudoku.sln

# Run a single test project
dotnet test Test/Test.csproj

# Run specific solver API
dotnet run --project Solve.Server/Solve.Server.csproj
# → http://localhost:5185/sudokusolve (Swagger UI available)

# Run web application
dotnet run --project WebUi/WebUi.csproj
# → http://localhost:<port>/sudokusolver

# Run desktop application
dotnet run --project Forms/Forms.csproj
```

## Solution Structure

23-project .NET 10.0 solution targeting `net10.0` (desktop apps: `net10.0-windows`).

| Project | Role |
|---|---|
| `Solve` | Core 9×9 solver engine — algorithms, model, extensions |
| `Solve.Abstraction` | DTOs shared between solver and clients |
| `Solve.Server` | ASP.NET Core Minimal API exposing solver over HTTP |
| `WebUi` | Razor Pages app with EF Core Identity auth + localization |
| `Repository` | EF Core data layer (SQLite default, SQL Server optional) |
| `Forms` | Windows Forms desktop UI |
| `Host` | Blazor WebAssembly host (Server / Client / Shared) |
| `Shared` | Shared utilities/models |
| `Test` | XUnit test suite |
| `Framework/` | 30+ reusable utilities (DI, logging, repository abstractions, WinAPI, etc.) |

## Architecture

### Solver Engine (`Solve/`)

`Sudoku` (582 lines) is the central 9×9 grid model. `SolverBase` is the abstract base for all algorithms. Solvers follow a **chain-of-responsibility** pattern — each specialized class eliminates candidates using one technique, then returns control for the next pass:

- **Basic:** `SolverBlockade1/2/3` — hidden singles, pairs, triples
- **Fish:** `SolverXWing`, `SolverSwordfish`, `SolverJellyfish`
- **Wings:** `SolverXYWing`, `SolverXYZWing`, `SolverWWing`
- **Subset:** `SolverBlockade2SubSet`

Behavior is composed via extension methods (`SudokuExtensions`, `SudokuFieldExtensions`) for serialization, rotation, mirroring, and display.

### Web API (`Solve.Server/`)

Minimal APIs — all endpoints are wired in `Program.cs`:
- `GET /api/sudoku` — current grid state
- `GET /api/sudoku/next` — compute next hint
- `GET /api/sudoku/set` — set a cell value
- `GET /api/sudoku/finish` — auto-complete
- `GET /api/sudoku/solutioncount` — count solutions (1-second timeout)
- `GET /api/sudoku/smart` — formatted print

CORS is enabled for SPA consumption.

### Web UI (`WebUi/`)

Razor Pages with ASP.NET Core Identity (confirmed-account required). Named `HttpClient` instances target multiple backends (localhost, Pi, Leo Cloud). Localization: `de`, `en-US`, `fr`.

### Data Layer (`Repository/`)

EF Core `SudokuContext` supports SQLite (default) and SQL Server. Default seed data loaded from `Category.csv` and `Sudoku.csv`. Migrations live in `Repository/Migrations/`.

### Framework (`Framework/`)

Reusable library referenced across projects. Key sub-modules: `Repository.Abstraction` (generic repo patterns), `WebAPI.Host`, `NLogTools` (NLog integration), `Localization`, `Dependency` (DI helpers), `Parser`, `WinAPI/Wpf`.

## Testing

- Framework: XUnit + FluentAssertions + NSubstitute
- Tests live in `Test/`
- Run a single test class: `dotnet test Test/Test.csproj --filter "FullyQualifiedName~ClassName"`

## Deployment

- **Windows binary:** GitHub Actions builds self-contained single-file `win-x86` WinForms app
- **Docker:** `Solve.Server/Dockerfile` (multi-stage, `linux/amd64`); pushed to Docker Hub as `sudokusolveserver:latest`
- **Kubernetes:** `leocloud-deploy-V3.yaml` in repo root
- **Systemd:** `WebUi` and `Solve.Server` include `Microsoft.Extensions.Hosting.Systemd`

## Adding New Features

- **New solver technique:** Subclass `SolverBase`, implement the elimination logic, register in the solver chain
- **New API endpoint:** Add to `Solve.Server/Program.cs`
- **Database schema change:** Add EF Core migration — `dotnet ef migrations add <Name> --project Repository`
