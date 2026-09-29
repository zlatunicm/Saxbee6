# Saxbee6

SaxBee is a beekeeping management web application. It is being rewritten from a legacy ASP.NET Core + AngularJS app into a .NET 8 Web API backend with a React (Vite) frontend.

## Build & run

Backend, from `backend/Saxbee6.Api`:
- Build: `dotnet build`
- Run: `dotnet run` (serves at `http://localhost:5000`; Swagger UI at `/swagger` in Development)
- Test: no test project exists (see Testing)

Frontend, from `frontend`:
- Dev server: `npm run dev`
- Build: `npm run build` (runs `tsc -b && vite build`)
- Lint: `npm run lint`
- Test: no test script is defined in `package.json`

## Constraints

- Backend target framework is `net8.0` (`backend/Saxbee6.Api/Saxbee6.Api.csproj`). Frontend package manager is npm (`frontend/package-lock.json` is committed; no `engines` field is pinned in `package.json`).
- `Saxbee6/` at the repo root is the legacy ASP.NET Core (`netcoreapp1.1`) + AngularJS application, kept for reference per README.md. Do not modify it and do not use it as a pattern for new backend or frontend code.
- Everything under `Saxbee6/wwwroot/lib/**` (angular, bootstrap, jquery, font-awesome, etc., managed via `bower.json`) is vendored third-party code and must never be edited.
- Never commit secrets or connection strings, and never print values from any `appsettings*.json` file, in either `backend/Saxbee6.Api/` or the legacy `Saxbee6/`.

## Architecture

- The backend is a single ASP.NET Core Web API project, `backend/Saxbee6.Api` — no separate domain/infrastructure class libraries. It is not part of `Saxbee6.sln`, which only contains the legacy `Saxbee6` project.
- Layout: `Controllers/` (Apiaries, Hives, Queens, Inspections, Treatments, Harvests, Feedings, Tasks), `Data/Models/` (EF Core entities), `Data/SaxBeeContext.cs` (DbContext), `DTOs/` (request/response records, one file per entity), `Program.cs` (minimal hosting model).
- Persistence: EF Core 8, SQLite by default (`Data Source=saxbee.db`), or SQL Server when `ConnectionStrings:DefaultConnection` is set in `appsettings.json`. Schema is created via `Database.EnsureCreated()` in `Program.cs`; no EF migrations were found.
- The frontend is a Vite + React + TypeScript SPA under `frontend/src`: `api/` (axios client and typed per-resource clients), `components/`, `pages/`.
- The frontend calls the backend through a hardcoded axios `baseURL` of `http://localhost:5000/api` (`frontend/src/api/client.ts`); `vite.config.ts` defines no dev-server proxy. The backend enables CORS for `http://localhost:5173` and `http://localhost:3000` (`Program.cs`).

## Testing

- No test projects exist anywhere under `backend/`.
- No test files or directories were found under `frontend/` either, and `package.json` defines no `test` script.
- Treat both sides as having no automated test coverage until test projects/scripts are added.

## Conventions

- Backend DTOs are C# records, grouped one file per entity in `DTOs/` (e.g. `ApiaryDtos.cs`, `HiveDtos.cs`).
- Backend enums are serialized as strings via a `JsonStringEnumConverter` registered in `Program.cs`.
- Frontend API access is centralized in `frontend/src/api/`: one module per resource (`apiaries.ts`, `hives.ts`, `harvests.ts`, `inspections.ts`, `tasks.ts`, `treatments.ts`), plus shared `client.ts` and `types.ts`.

## Key docs

- [README.md](README.md) — project overview, features, tech stack, getting-started steps, and the REST API endpoint reference.
- [frontend/README.md](frontend/README.md) — default Vite + React + TypeScript template notes (ESLint configuration options).
