using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingRepository _shoppingRepository;

        public ShoppingController(IShoppingRepository shoppingRepository)
        {
            _shoppingRepository = shoppingRepository ?? throw new ArgumentNullException(nameof(shoppingRepository));
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var shoppingModel = await _shoppingRepository.GetShopping(userName);

            return Ok(shoppingModel);
        }
    }
}
