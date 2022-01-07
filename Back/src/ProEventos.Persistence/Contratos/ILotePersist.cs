using System.Threading.Tasks;
using ProEventos.Domain; 

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        //lotes        

        /// <summary>
        /// Método que retornará uma lista de Eventos por eventoId.
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <returns>Array de lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        /// Método que retornará apenas 1 lote.
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <param name="loteId">Código chave do meu lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}