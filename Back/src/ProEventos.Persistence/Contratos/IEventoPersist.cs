using System.Threading.Tasks;
using ProEventos.Domain; 

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    { 
        //EVENTOS
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrante = false);
        Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrante = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrante = false);
    }
}