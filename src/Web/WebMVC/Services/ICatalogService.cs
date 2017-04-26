using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetCatalogItems(int page, int take);
    }
}
