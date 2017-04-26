using Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Infrastructure.Repositories;
using Catalog.API.Model;
using Catalog.API.ViewModel;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly CatalogSettings _settings;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogController(ICatalogRepository repository, IOptionsSnapshot<CatalogSettings> settings, ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));

            _settings = settings.Value;
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)

        {
            var totalItems = _catalogRepository.All().LongCount();

            var itemsOnPage = _catalogRepository.All()
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

            var model = new PaginatedItemsViewModel<Beer>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [HttpGet]
        [Route("items/{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = _catalogRepository.All().SingleOrDefault(ci => ci.Id == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> Items(string name, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {

            var totalItems = _catalogRepository.All()
                .Where(c => c.Name.StartsWith(name))
                .LongCount();

            var itemsOnPage = _catalogRepository.All()
                .Where(c => c.Name.StartsWith(name))
                .Skip(pageSize * pageIndex)
                .Take(pageSize);


            var model = new PaginatedItemsViewModel<Beer>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //DELETE api/v1/[controller]/id
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = _catalogRepository.All().SingleOrDefault(x => x.Id == id);

            if (beer == null)
            {
                return NotFound();
            }

            _catalogRepository.Remove(beer);

            return NoContent();
        }
    }
}
