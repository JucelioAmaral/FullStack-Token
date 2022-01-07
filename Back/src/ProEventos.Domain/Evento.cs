using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{

    //Todos os comentários abaixo são referentes ao Data Annotation EF Core, ou seja, são voltados para uso junto do BD.

    //[Table("OutroNome")] -> Fica nesse local mesmo, antes do nome da classe.
    //-> Funciona como o decorator do DataAnnotation, porém serve para colocar um nome diferente na tabela
    //que será criada no banco de dados, nesse caso "OutroNome". Importar: using System.ComponentModel.DataAnnotations.Schema;

    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        //[MaxLength(50)] -> Determina o tamanho do campo no banco de dados.
        public string Tema { get; set; }        
        public int QtdPessoas { get; set; }        
        public string ImagemURL { get; set; }        
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }

        //[NotMapped]
        //public int ContagemDias { get; set; }
        //-> Essa propriedade abaixo serve como exemplo de propriedade que se fosse da classe não seria mapeada, ou seja, não seria
        // inseriada/criada na tabela do banco de dados devido o decorator "[NotMapped]".


    }
}