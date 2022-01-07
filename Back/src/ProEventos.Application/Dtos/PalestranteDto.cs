using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    class PalestranteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                   ErrorMessage = "Não é uma imagem válida. (gif,jpg,jpeg,bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]// não permite entrada de letras.
        public string Telefone { get; set; }

        [Required(),
         Display(Name = "e-mail"),
         EmailAddress(ErrorMessage = "O campo {0} precisa ser um e-mail válido.")
        ]
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}
