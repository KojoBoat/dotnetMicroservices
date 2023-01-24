using Basket.Api.Entities;
using Basket.Api.GrpcServices;
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
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository, ILogger<ShoppingCart> logger, DiscountGrpcService discountGrpcService)
        {
            this.basketRepository = basketRepository;
            this.logger = logger;
            _discountGrpcService = discountGrpcService;
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

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart cart)
        {
            if(cart == null) return NotFound($"No cart found!");

            //TODO: Consume Discount.GRPC and calculate final price of product
            foreach (var item in cart.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
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
