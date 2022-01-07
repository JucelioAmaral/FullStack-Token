using System.Threading.Tasks;
using ProEventos.Domain; 

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        //lotes        

        /// <summary>
        /// M�todo que retornar� uma lista de Eventos por eventoId.
        /// </summary>
        /// <param name="eventoId">C�digo chave da tabela Evento</param>
        /// <returns>Array de lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        /// M�todo que retornar� apenas 1 lote.
        /// </summary>
        /// <param name="eventoId">C�digo chave da tabela Evento</param>
        /// <param name="loteId">C�digo chave do meu lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}