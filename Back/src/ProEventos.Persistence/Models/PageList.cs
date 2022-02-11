using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Models
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList()
        {

        }

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber == 0)// Tratativa paliativa, indicada pelo professor na aula 251 da versão 2.
                pageNumber = 1;

            if (pageSize == 0)// Tratativa paliativa, indicada pelo professor na aula 251 da versão 2.
                pageSize = 1;

            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize) //o -1 é devido o Array começar no 0()zero, ou seja, página 1, na verdade é zero. A multiplicação é para pular q qtde de itens por página para chegar onde você escolheu.
                                    .Take(pageSize)// pega tais itens que estão na página escolhida, após a multiplicação.
                                    .ToListAsync();

            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
