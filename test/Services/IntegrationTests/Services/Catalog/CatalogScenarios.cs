namespace IntegrationTests.Services.Catalog
{
    using System.Threading.Tasks;
    using Xunit;

    public class CatalogScenarios
        : CatalogScenarioBase
    {
        [Fact]
        public async Task Get_get_all_catalogitems_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Items);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_get_paginated_catalog_items_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Paginated(0, 4));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
