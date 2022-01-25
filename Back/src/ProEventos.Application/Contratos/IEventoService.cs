using System.Threading.Tasks;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEventos(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);

        Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrante = false);
        
        Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrante = false);
    }
}