using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Queries;
using Market.ReadModels.Models;
using Market.ReadModels.Read.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductQueriesController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public ProductQueriesController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct(GetAllProducts query)
        {
             var products = await _queryProcessor.QueryAsync<IReadOnlyList<ProductReadModel>>(query);
             return Ok(products);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPageProduct(GetProducts query)
        {
            var productsForPageAndCountAll = await _queryProcessor.QueryAsync<(IReadOnlyList<ProductReadModel> page, int pageCount)>(query);
            return Ok(productsForPageAndCountAll);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductById(GetProductById query)
        {
            var product = await _queryProcessor.QueryAsync<ProductReadModel>(query);
            return Ok(product);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProductCount(GetAllProductCount query)
        {
            var count = await _queryProcessor.QueryAsync<int>(query);
            return Ok(count);
        }

        [HttpGet("[action]")]
        //todo fix problem i can see item1 and item2 i cannot see page and pageCount field
        [ProducesResponseType(typeof((IReadOnlyList<ProductReadModel> page, int pageCount)), 200)]
        public async Task<IActionResult> GetFilteredProductPage(GetFilteredProducts query)
        {
            var productsForPageAndCountAll = await _queryProcessor.QueryAsync<(IReadOnlyList<ProductReadModel> page, int pageCount)>(query);
            return Ok(productsForPageAndCountAll);
        }
    }
}
