using System.Threading;
using FlexD.Contracts.Interfaces.Infrastructure;

namespace FlexD.Http;

public class HttpMessageCommandHandler : IRequestHandler<HttpMessageCommand, int>
{
    private readonly IInfrastructureOutputWriter _outputWriter;
    public HttpMessageCommandHandler(IInfrastructureOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }
    public Task<int> Handle(HttpMessageCommand request, CancellationToken cancellationToken)
    {
        _outputWriter.WriteFaintLine($"Calling {request.Endpoint} and then will swop out based on the marker {request.Marker}");
        return Task.FromResult<int>(0);
    }
}