using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using FlexD.Http;
using FlexD.Contracts.Interfaces.Infrastructure;

namespace FlexD.CLI.Tests;

public class MessageHandlerTests
{
    public MessageHandlerTests()
    {
        ConfigureEnvironmentVariablesFromLocalSettings();
    }

    [Fact]
    public async Task MessageHandler_Should_Return0_WithBasicSetup()
    {
        var writer = new Mock<IInfrastructureOutputWriter>();
        
        var messageHandler = new HttpCommandHandler(writer.Object);
        var result = await messageHandler.Handle(new HttpCommand()
        {
            Endpoint = "HTTP://Foo.com",
            Marker = "+++",
        }, new CancellationToken());
        
        Assert.True(result == 0);
    }
    static void ConfigureEnvironmentVariablesFromLocalSettings()
    {
        var path = Path.GetDirectoryName(typeof(MessageHandlerTests).Assembly.Location);
        var json = File.ReadAllText(Path.Join(path, "local.settings.json"));
        var parsed = Newtonsoft.Json.Linq.JObject.Parse(json).Value<Newtonsoft.Json.Linq.JObject>("Values");

        foreach (var item in parsed)
        {
            Environment.SetEnvironmentVariable(item.Key, item.Value.ToString());
        }
    }
}
