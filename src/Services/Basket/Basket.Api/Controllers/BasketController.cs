using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly ILogger<ShoppingCart> logger;

        public BasketController(IBasketRepository basketRepository, ILogger<ShoppingCart> logger)
        {
            this.basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{username}", Name = "GetCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetCart(string username)
        {
            if (string.IsNullOrEmpty(username)) return NotFound(username);
            var result = await basketRepository.GetCart(username);
            //if (result == null)
            //{
            //    logger.LogError($"Error fetching cart for {username}");
            //    return NotFound($"Cart not found for {username}");
            //}
            return Ok(result ?? new ShoppingCart(username));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart cart)
        {
            if(cart == null) return NotFound($"No cart found!");
            var result = await basketRepository.UpdateCart(cart);
            return Ok(result);
        } 
        
        [HttpDelete("{username}", Name = "DeleteCart")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string username)
        {
            if(string.IsNullOrEmpty(username)) return NotFound(username);
            await basketRepository.DeleteCart(username);
            return Ok();
        }
    }
}
