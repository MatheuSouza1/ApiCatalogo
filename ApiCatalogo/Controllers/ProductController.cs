using ApiCatalogo.Context;
using ApiCatalogo.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCatalogo.Repository;
using AutoMapper;
using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IUnitOfWork _uof;
        public readonly IMapper _mapper;
        public ProductController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper= mapper;
        }

        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProdutosPrecos()
        {
            var products = await _uof.ProdutoRepository.GetProductsPorPreco();
            var productsDTO = _mapper.Map<List<ProductsDTO>>(products);
            return productsDTO;
        }

        //paginada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> Get([FromQuery] ProductParameters productParameters)
        {
            var products = await _uof.ProdutoRepository.GetProducts(productParameters);

            var metadata = new
            {
                products.TotalCount, products.PageSize, products.CurrentPage, products.TotalPages, products.HasNext, products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var productsDTO = _mapper.Map<List<ProductsDTO>>(products);
            return productsDTO;
        }

        [HttpGet("{id:int}", Name = "ObterObjeto")]
        public async Task<ActionResult> Get(int id)
        {
            var product = await _uof.ProdutoRepository.GetById(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductsDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductsDTO productDTO)
        {
            var products = _mapper.Map<Product>(productDTO);
            _uof.ProdutoRepository.Add(products);
            await _uof.Commit();
            var productDto = _mapper.Map<ProductsDTO>(products);
            return new CreatedAtRouteResult("ObterObjeto", new { id = products.ProductId, productDto});
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProductsDTO productDTO)
        {
            if (id != productDTO.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDTO);
            _uof.ProdutoRepository.Update(product);
            await _uof.Commit();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _uof.ProdutoRepository.GetById(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _uof.ProdutoRepository.Delete(product);
            await _uof.Commit();
            return Ok();
        }
    }
}
