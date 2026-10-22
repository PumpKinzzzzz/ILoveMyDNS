using System.Xml.Linq;

namespace DnsCore.Server.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Server_DoesNotUseProtocolTypes()
    {
        var referencedAssemblies = typeof(Worker).Assembly
            .GetReferencedAssemblies()
            .Select(assembly => assembly.Name);

        Assert.DoesNotContain("DnsCore.Protocol", referencedAssemblies);
    }

    [Fact]
    public void Server_DoesNotReferenceProtocolDirectly()
    {
        var serverProject = XDocument.Load(FindServerProjectFile());

        var projectReferences = serverProject
            .Descendants("ProjectReference")
            .Select(reference => (string?)reference.Attribute("Include") ?? string.Empty)
            .ToList();

        Assert.DoesNotContain(projectReferences, path => path.Contains("DnsCore.Protocol", StringComparison.Ordinal));
    }

    private static string FindServerProjectFile()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "DnsCore.sln")))
        {
            directory = directory.Parent;
        }

        Assert.NotNull(directory);
        return Path.Combine(directory.FullName, "src", "DnsCore.Server", "DnsCore.Server.csproj");
    }
}
