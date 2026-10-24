namespace DnsCore.Server.Tests;

public class BrokenTest
{
    [Fact]
    public void Deliberately_Fails()
    {
        Assert.Equal(1, 2);
    }
}
