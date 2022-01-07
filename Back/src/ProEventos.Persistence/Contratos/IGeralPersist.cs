using System.Threading.Tasks;
using ProEventos.Domain; 

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersist
    {  
       // GERAL, para todos os eventos abaixo de qq classe.
        void Add<T>(T entity) where T: class ;
        void Update<T>(T entity) where T: class ;
        void Delete<T>(T entity) where T: class ;
        void DeleteRange<T>(T entity) where T: class ; 
        Task<bool> SaveChangesAsync();
    }
}