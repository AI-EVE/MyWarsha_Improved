using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ProductToSellDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ProductToSellFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsToSellController : ControllerBase
    {
        private readonly IProductToSellRepository _productToSellRepository;

        public ProductsToSellController(IProductToSellRepository productToSellRepository)
        {
            _productToSellRepository = productToSellRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties, [FromQuery] ProductToSellFilters productToSellFilters)
        {
            var productsToSell = await _productToSellRepository.GetAll(productToSellFilters.GetExpression(), paginationPropreties);
            return Ok(productsToSell);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var productToSell = await _productToSellRepository.Get(x => x.Id == id);
            return Ok(productToSell);
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCount([FromQuery] ProductToSellFilters productToSellFilters)
        {
            var count = await _productToSellRepository.GetCount(productToSellFilters.GetExpression());
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductToSellCreateDto productToSellDto)
        {
            var productToSell = new ProductToSell
            {
                PricePerUnit = productToSellDto.PricePerUnit,
                Discount = productToSellDto.Discount,
                Count = productToSellDto.Count,
                Note = productToSellDto.Note,
                ProductId = productToSellDto.ProductId,
                ServiceId = productToSellDto.ServiceId
            };

            await _productToSellRepository.Add(productToSell);
            await _productToSellRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = productToSell.Id }, null);
        }
        
        [HttpPost("bulk")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBulk([FromBody] IEnumerable<ProductToSellCreateDto> productToSellDtos)
        {
            var productsToSell = productToSellDtos.Select(x => new ProductToSell
            {
                PricePerUnit = x.PricePerUnit,
                Discount = x.Discount,
                Count = x.Count,
                Note = x.Note,
                ProductId = x.ProductId,
                ServiceId = x.ServiceId
            });

            await _productToSellRepository.AddRange(productsToSell);
            await _productToSellRepository.SaveChanges();

            return CreatedAtAction(nameof(GetAll), null, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductToSellUpdateDto productToSellDto)
        {
            var productToSell = await _productToSellRepository.GetById(id);
            if (productToSell == null)
            {
                return NotFound();
            }

            productToSell.PricePerUnit = productToSellDto.PricePerUnit ?? productToSell.PricePerUnit;
            productToSell.Discount = productToSellDto.Discount ?? productToSell.Discount;
            productToSell.Count = productToSellDto.Count ?? productToSell.Count;
            productToSell.Note = productToSellDto.Note ?? productToSell.Note;
            productToSell.IsReturned = productToSellDto.IsReturned ?? productToSell.IsReturned;

            await _productToSellRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var productToSell = await _productToSellRepository.GetById(id);
            if (productToSell == null)
            {
                return NotFound();
            }

            _productToSellRepository.Delete(productToSell);
            await _productToSellRepository.SaveChanges();
            return NoContent();
        }
    }
}