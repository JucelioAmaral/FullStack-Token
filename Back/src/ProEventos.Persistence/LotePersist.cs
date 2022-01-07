using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly ProEventosContext _context;

        public LotePersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote[]>GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.tblLotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote>GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.tblLotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId
                         && lote.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}