# ILoveMyDNS

[![CI](https://github.com/PumpKinzzzzz/ILoveMyDNS/actions/workflows/ci.yml/badge.svg)](https://github.com/PumpKinzzzzz/ILoveMyDNS/actions/workflows/ci.yml)

A DNS server written in C# on .NET 8, built in the open. The goal is to understand the protocol byte by byte, learn the platform properly, and keep the project in a shape someone else could pick up.

The code lives in a solution called `DnsCore`.

## Status

Early. Nothing answers a DNS query yet. The solution builds, the CI runs, and `DnsCore.Server` is still the stock Worker Service template that logs a line every second. The real work starts with the protocol library.

## Roadmap

Work is tracked in GitHub issues, grouped by milestone.

| Milestone | Goal | State |
|---|---|---|
| M0 Foundations | Solution layout, shared build rules, CI, coverage, repository docs | In progress |
| M1 Protocol | Parse and serialize DNS messages (RFC 1035), including name compression and malformed input | Not started |

Nothing is planned past M1. The server loop, caching and recursion come once the protocol layer is solid.

## Layout

```
src/
  DnsCore.Protocol   DNS messages: parsing and serialization
  DnsCore.Resolver   Turns a request into a response (builds on Protocol)
  DnsCore.Server     The daemon: sockets in, bytes out (builds on Resolver)
tests/               One xUnit project per source project
bench/               Benchmarks (an empty console project for now)
```

Server talks to Resolver and never to Protocol directly. An architecture test fails the build if that changes.

## Build and test

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
git clone https://github.com/PumpKinzzzzz/ILoveMyDNS.git
cd ILoveMyDNS
dotnet restore
dotnet build
dotnet test
```

Warnings are errors, so a clean build means zero warnings.

Check formatting the way the CI does:

```bash
dotnet format --verify-no-changes
```

Generate a coverage summary:

```bash
dotnet tool restore
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings --results-directory coverage
dotnet reportgenerator -reports:"coverage/**/coverage.cobertura.xml" -targetdir:coverage/report -reporttypes:MarkdownSummaryGithub
```

The summary lands in `coverage/report/SummaryGithub.md`. On GitHub the same summary appears on the page of each CI run. There is no coverage badge: a live one needs an external service, and a hand-written number would go stale.

## Contributing

Read [CONTRIBUTING.md](CONTRIBUTING.md) for commit format, branch names and the pull request flow.

## License

[MIT](LICENSE). It is the simplest license that lets anyone read, reuse and learn from the code.
