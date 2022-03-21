using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Models
{
    public class PageParams
    {
        public const int MaxPageSize = 50; // Itens por p�gina. Pode aumentar ou diminuir se quiser.
        public int PageNumber { get; set; } = 1;
        public int pageSize = 10; //N�mero m�ximo de p�ginas. Pode aumentar ou diminuir se quiser.
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Term { get; set; } = string.Empty;
    }
}