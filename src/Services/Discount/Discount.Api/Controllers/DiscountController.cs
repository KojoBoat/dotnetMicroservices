using Discount.Api.Entities;
using Discount.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            if (string.IsNullOrEmpty(productName)) return BadRequest(productName);

            var action = await _repository.GetDiscount(productName);
            if (action == null) return NotFound(productName);

            return Ok(action);
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var inserted = await _repository.CreateDiscount(coupon);
            if (inserted) return CreatedAtRoute("GetDiscount", new { coupon.ProductName }, coupon);

            return BadRequest(coupon);
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            var updateDiscount = await _repository.UpdateDiscount(coupon);
            if (updateDiscount) return CreatedAtRoute("GetDiscount", new
            {
                coupon.ProductName
            }, coupon);

            return NotFound(coupon);
        }

        [HttpDelete("{productName}")]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            if (string.IsNullOrEmpty(productName)) return BadRequest();

            var coupon = await _repository.DeleteDiscount(productName);
            if (coupon) return Ok(coupon);
            return NotFound(productName);
        }
    }
}
