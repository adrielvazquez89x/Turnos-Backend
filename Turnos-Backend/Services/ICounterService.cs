using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public interface ICounterService
    {
        int GetNextCounter(int customerType);
    }
}
