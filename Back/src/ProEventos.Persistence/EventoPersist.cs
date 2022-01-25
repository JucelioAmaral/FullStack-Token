using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersist(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.tblEventos //LE-SE: Para cada evento na tabela Evento, inclui os lotes e as redes sociais.
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrante)// se "includePalestrante" for verdadeiro, inclui também, na query, o palestranteEvento e inclui o palestrante.
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)// Include = Inclui. Ou seja, são SELECT's.
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                         .Where(e => e.Tema.ToLower().Contains(pageParams.Term.ToLower()) && e.UserId == userId)
                         .OrderBy(e => e.Id);// Ordenado por Id.

            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.tblEventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id)
                                        .Where(e => e.Id == eventoId && e.Id == userId);

            return await query.FirstOrDefaultAsync();
        }       
    }
}