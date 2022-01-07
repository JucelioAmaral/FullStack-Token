using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<int>//o int representa o TKey, ou seja, o ID da tabela, o mesmo que uma referencia. E, será incremental (identity).
    {
        //[Column(TypeName = "nvarchar(150)")]

        //public string fullName { get; set; }// Além dos atributos que já exsitem em "IdentityUser", será criada uma coluna com esse nome na tabela de usuário.

        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; }
        public Funcao Funcao { get; set; }
        public string ImagemURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
