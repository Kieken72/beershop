using System.Collections.Generic;
using WebMVC.ViewModels.Pagination;

namespace WebMVC.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
