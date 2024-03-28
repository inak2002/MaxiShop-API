using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ViewModels
{
    public class PaginationVM<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalNoRecords { get; set; }
        public List <T> Items { get; set; }
        public bool hasprevious =>CurrentPage>1;
        public bool hasnext =>CurrentPage < TotalPages;

        public PaginationVM(int currentPage,int totalPages,int pageSize,int totalnorecords,List<T> items)
        {
            CurrentPage=currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalNoRecords = totalnorecords;
            Items = items;
        }
    }
}
