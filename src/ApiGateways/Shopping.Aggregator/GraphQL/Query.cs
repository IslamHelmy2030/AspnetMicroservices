using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.GraphQL
{
    public class Query
    {
        private readonly ICatalogService _catalogService;

        public Query(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            return await _catalogService.GetCatalog();
        }
    }
}
