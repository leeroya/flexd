using FlexD.Contracts.Interfaces.Options;


namespace FlexD.CLI.Tests;

public class ConnectionProperties: IConnectionProperties
{
  public string? ConnectionString { get; set; }
}