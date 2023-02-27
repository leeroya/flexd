using CommandLine;

namespace FlexD.Contracts.Interfaces.Options;

public interface IConnectionProperties
{
    [Option('c', "connection-string", SetName = "ConnectionString", HelpText = "The SQL connection string to use when connecting to the database.")]
    public string ConnectionString { get; set; }
}