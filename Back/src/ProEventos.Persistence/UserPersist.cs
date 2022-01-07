using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersist : GeralPersist,  IUserPersist
    {
        private readonly ProEventosContext _context;

        public UserPersist(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAcync()
        {
            return await _context.Users.ToListAsync();// Esse "Users" não é uma tabela do Banco de dados, ele é do IdentityDbContext.
        }

        public async Task<User> GetUserByIdsAcync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByNameAcync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }

    }
}
