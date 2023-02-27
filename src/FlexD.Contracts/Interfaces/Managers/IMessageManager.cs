using System.Threading.Tasks;

namespace FlexD.Contracts.Interfaces.Managers;

public interface IMessageManager
{
    Task<bool> Gooi(string message);
}