using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Commands;
using Market.Domain.Products.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductCommandsController : ControllerBase
    {
        private readonly ICommandSender _commandSender;

        public ProductCommandsController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody]CreateProductCommand command)
        {
           await _commandSender.SendAsync(command);
           return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProductImages([FromBody] AddProductImageCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveProduct([FromBody] DeleteProductCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveProductImage([FromBody] DeleteProductImageCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductName([FromBody] ChangeProductNameCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductPrice([FromBody] ChangeProductPriceCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductBrand([FromBody] ChangeProductBrandCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductColor([FromBody] ChangeProductColorCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductType([FromBody] ChangeProductTypeCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeProductDiscount([FromBody] ChangeProductDiscountCommand command)
        {
            await _commandSender.SendAsync(command);
            return Ok();
        }

        
    }
}
