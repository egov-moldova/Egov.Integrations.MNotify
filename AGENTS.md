# AI Agents Integration

This repository is designed to be friendly for AI-assisted development and automation agents.

## Project Structure

- `src/Egov.Integrations.MNotify/`: The base library source code.
- `src/Egov.Integrations.MNotify.Extended/`: The extended (recipient-facing) library. `MNotifyExtendedClient` derives from the base `MNotifyClient` and uses its `internal` members, granted by `InternalsVisibleTo("$(AssemblyName).Extended")` in the base project — the assembly name must not change. Both projects are packed and published together by `publish.yml`.
- `src/Egov.Integrations.MNotify.Tests/`: The xUnit test project.
- `src/Test/`: A sample console app for manual smoke-testing against a real MNotify endpoint.
- `src/files/`: Packaging assets (NuGet icon), shared by both packages.

## Developer Instructions

- Follow the `.editorconfig` for code style.
- Ensure all public APIs are documented with XML comments.
- Run `dotnet test` before submitting changes.
- Use `dotnet pack -c Release` to verify NuGet package output.
