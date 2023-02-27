using MediatR;
using FlexD.Core.Options;

namespace FlexD.Core;
public class MessageCommand: IRequest<int>, ICommandOptions
{
  [Option("Message", HelpText = "Provide a string message that can be sent.")]
  public string? Message { get; set; } 
}
