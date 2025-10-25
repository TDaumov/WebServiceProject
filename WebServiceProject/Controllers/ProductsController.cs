using Microsoft.AspNetCore.Mvc;
using WebServiceProject.Models;
using WebServiceProject.Repositories.Interfaces;

namespace WebServiceProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _repo.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }


        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.Add(product);
            _repo.Save();

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }


        //[HttpPost]
        //public IActionResult Create(Product product)
        //{
        //    _repo.Add(product);
        //    _repo.Save();
        //    return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        //}

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            _repo.Update(product);
            _repo.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return NoContent();
        }
    }
}
