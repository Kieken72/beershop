using System.Collections.Generic;
using Catalog.API.Model;

namespace Catalog.API.Infrastructure.Repositories
{
    public interface ICatalogRepository
    {
        IEnumerable<Beer> All();
        void Remove(Beer beer);
    }
}