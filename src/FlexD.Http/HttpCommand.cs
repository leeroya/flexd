namespace FlexD.Http;

[Verb("http-source", HelpText = "Executes a HTTP Get and replaces host file node with result.")]
public class HttpCommand: IRequest<int>, IHttpCommandOptions
{
    [Option("Endpoint", SetName = "HTTP", HelpText = "Url to retrieve the required host file contents.")]
    public string? Endpoint { get; set; }
    
    [Option("Marker", SetName = "HTTP", HelpText = "The token used to manage where in the host file to maintain.")]
    public string? Marker { get; set; }

}
