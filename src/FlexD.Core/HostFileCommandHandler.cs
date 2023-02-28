using FlexD.Contracts.Interfaces.Infrastructure;
using MediatR;

namespace FlexD.Core;

public class HostFileCommandHandler: IRequestHandler<HostFileCommand, int>
{
    private readonly IInfrastructureOutputWriter _outputWriter;
    public HostFileCommandHandler(IInfrastructureOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }
    public Task<int> Handle(HostFileCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult<int>(0);
    }
}