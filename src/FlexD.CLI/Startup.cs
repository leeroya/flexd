
using FlexD.Contracts.Interfaces.Infrastructure;
using FlexD.Http;
using Spectre.Console;

namespace FlexD.CLI;
public class Startup
{
  private readonly IHost _host;
  private readonly IMediator _mediator;
  private readonly IInfrastructureOutputWriter _outputWriter;
  public Startup(IMediator mediator, IInfrastructureOutputWriter outputWriter, IHost host)
  {
    _mediator = mediator;
    _outputWriter = outputWriter;
    _host = host;
  }

  public async Task<int> Execute(string[] args, CancellationToken cancellationToken = default)
  {
    Parsed<object>? result = ParseArguments(args);
    if (result != null && result.Value is IHttpCommandOptions command)
    {
      await _mediator.Send(command, cancellationToken);
      _outputWriter.WriteCompletedMessage($"{Emoji.Known.Detective}  Gooi (eng: Throw) is done!");
      return 0;
    }

    if (args.Contains("--help") || args.Contains("--version"))
    {
      return 0;
    }

    return 1;
  }
  private Parsed<object>? ParseArguments(string[] args)
  {
    var parser = new Parser(settings =>
    {
      settings.EnableDashDash = true;
      settings.CaseSensitive = false;
      settings.HelpWriter = System.Console.Out;
      settings.AutoVersion = false;
    });
    
    var commands = _host.Services.GetServices<IHttpCommandOptions>().ToList();

    if (!commands.Any())
    {
      throw new InvalidOperationException($" {Emoji.Known.WorriedFace} Well this is embarrassing. Gooi (eng: Throw) does not know what to do.");
    }

    Type[] types = commands.Select(s => s.GetType()).ToArray();
    var result = parser.ParseArguments(args, types) as Parsed<object>;

    // Display custom help
    result.WithNotParsed(errs =>
    {
      if (result != null) DisplayHelp(result, errs, types);
    });
    return result;
  }
  private void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs, Type[] types)
  {
    if (errs.IsVersion())
    {
      // Do Nothing since we are always printing the version.
      return;
    }

    var helpText = HelpText.AutoBuild(result,
      settings =>
      {
        settings.AdditionalNewLineAfterOption = false;
        settings.Heading = string.Empty;
        settings.Copyright = string.Empty;
        settings.AddVerbs(types);
        settings.AddDashesToOption = true;
        settings.AutoVersion = false;
        settings.MaximumDisplayWidth = GetMaximumWindowWidth();
        return HelpText.DefaultParsingErrorsHandler(result, settings);
      },
      onExample: e => e,
      verbsIndex: true);

    _outputWriter.WriteLine(helpText);
  }

  private int GetMaximumWindowWidth()
  {
    var defaultMaximumLength = 80;
    int maximumDisplayWidth;
    try
    {
      maximumDisplayWidth = Console.WindowWidth;
      if (maximumDisplayWidth < 1)
      {
        return defaultMaximumLength;
      }
    }
    catch (IOException)
    {
      return defaultMaximumLength;
    }

    return maximumDisplayWidth;
  }
}
