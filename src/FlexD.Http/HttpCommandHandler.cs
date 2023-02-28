using FlexD.Contracts.Interfaces.Infrastructure;

namespace FlexD.Http;

public class HttpCommandHandler : IRequestHandler<HttpCommand, int>
{
    private readonly IInfrastructureOutputWriter _outputWriter;
    public HttpCommandHandler(IInfrastructureOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }
    public Task<int> Handle(HttpCommand request, CancellationToken cancellationToken)
    {
        _outputWriter.WriteFaintLine($"Calling {request.Endpoint} and then will swop out based on the marker {request.Marker}");
        return Task.FromResult<int>(0);
    }
}