using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.ViewModels.Pagination;
using WebMVC.Services;
using WebMVC.ViewModels.CatalogViewModels;


namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;

        public CatalogController(ICatalogService catalogSvc) => 
            _catalogSvc = catalogSvc;

        public async Task<IActionResult> Index(int? page)
        {
            var itemsPage = 10;
            var catalog = await _catalogSvc.GetCatalogItems(page ?? 0, itemsPage);
            var vm = new IndexViewModel()
            {
                CatalogItems = catalog.Data,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = catalog.Data.Count,
                    TotalItems = catalog.Count, 
                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }

        public IActionResult Error() => View();
    }
}

