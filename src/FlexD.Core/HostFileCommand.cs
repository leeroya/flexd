using MediatR;
using FlexD.Core.Options;

namespace FlexD.Core;
public class HostFileCommand: IRequest<int>, IHostFileCommandOptions
{
  [Option("file-contents", SetName = "HostFile", HelpText = "Set Host file contents.")]
  public string? FileContents { get; set; }
}
