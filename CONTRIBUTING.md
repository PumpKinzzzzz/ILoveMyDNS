# Contributing

Small changes, each one built, tested and explained. That is the whole philosophy.

## Before you start

Pick an issue, or open one. Work is tracked in GitHub issues, grouped by milestone. Build and test locally first, using the steps in the [README](README.md#build-and-test).

## Commits

Commits follow [Conventional Commits](https://www.conventionalcommits.org/):

```
type(scope): short description in the imperative
```

No space before the parenthesis. Keep the subject under 72 characters and leave off the final period. Use the body to say why, not to list files; git already knows the files.

Types: `feat`, `fix`, `docs`, `test`, `perf`, `refactor`, `build`, `ci`, `chore`.

Scopes: `protocol`, `resolver`, `server`, `ci`, `docs`. Leave the scope out when a change touches the whole repository.

```
feat(protocol): parse the DNS header
fix(resolver): return NXDOMAIN for unknown names
docs: explain the architecture rule
```

One commit is one change that works on its own. A commit that breaks the build makes `git bisect` useless.

## Branches

Name branches `type/number-description`, where the number is the issue:

```
feat/9-header-parsing
docs/5-readme-license-contributing
```

The name GitHub generates from the issue page (`5-readme-license-and-contributing`) is accepted too. Branch from `main`.

## Pull requests

1. Push your branch and open a pull request against `main`.
2. Link the issue. Write `Closes #n` when the pull request finishes it, `Part of #n` when work remains. `Closes` shuts the issue at merge, so use it only when it is true.
3. Wait for the `build-and-test` check. `main` is protected: it takes a pull request and a green check. Nobody pushes to it directly.
4. Merge with a merge commit or a rebase. Squash is disabled, because it would flatten the commit history you just wrote.
5. Delete the branch.

## Rules the code enforces

- Warnings are errors. A build with a warning fails.
- Formatting follows `.editorconfig`. Run `dotnet format` before pushing; the CI runs `dotnet format --verify-no-changes`.
- `DnsCore.Server` must not reference or use types from `DnsCore.Protocol`. Two architecture tests in `DnsCore.Server.Tests` check it.
- Tests use xUnit and live in the `tests/` project that matches the code under test.

## Dependencies

Package versions are set once, in `Directory.Packages.props`. A project file lists the package name and no version. Build rules shared by every project sit in `Directory.Build.props`.
