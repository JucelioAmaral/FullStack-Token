using ProEventos.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        //Required, Display, EmailAddress...são decoraters do dataAnnotations e não tem ordem a ser colocados.

        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        
        [Required(ErrorMessage ="O campo {0} é obrigatório."),
            //MinLength(3, ErrorMessage ="O {0} deve conter no mínimo 4 caracteres"),
            //MaxLength(50, ErrorMessage = "O {0} deve conter no mínimo 50 caracteres"),
            StringLength(50, MinimumLength = 3,
                             ErrorMessage ="Intervalo permitido de 3 a 50 caracteres")
        ]
        public string Tema { get; set; }

        [Display(Name="Qtd Pessoas")]
        [Range(1,120000, ErrorMessage ="{0} não pode ser menor que 1 e maior que 120.000.")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                           ErrorMessage = "Não é uma imagem válida. (gif,jpg,jpeg,bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Phone(ErrorMessage ="O campo {0} está com número inválido")]// não permite entrada de letras.
        public string Telefone { get; set; }

        //[Required(),
        // Display(Name ="e-mail"),
        // EmailAddress(ErrorMessage ="O campo {0} precisa ser um e-mail válido.")
        //]
        // Pode ser usado com da forma de sima ou da de baixo, usando 1 colchete "[]" para tudo ou 1 colchete para cada linha, igual abaixo.
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "e-mail")] // Será exibida na mensagem o nome do Display, ao invés do nome escrito no atributo.
        [EmailAddress(ErrorMessage = "O campo {0} precisa ser um e-mail válido.")]
        public string Email { get; set; }

        public int UserId { get; set; }

        public UserDto UserDto { get; set; }

        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> palestrantesEventos { get; set; }
    }
}
