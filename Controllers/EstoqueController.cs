using System;
using System.Threading.Tasks;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{   
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly ProdutoRepository _repository;
        
        public EstoqueController(ProdutoRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProdutos()
        {
            try
            {
                var produtosList = await _repository.GetAllProdutos();
                if (produtosList == null) return NotFound();
                return Ok(produtosList);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBodyProdutos([FromBody] Produto produtoId)
        {
            try
            {
                var produto = await _repository.GetProdutoById(produtoId.Id);
                if (produto == null) return NotFound();
                return Ok(produto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }

        
    }
}