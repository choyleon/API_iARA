using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAspNetCore6.Context;
using ApiAspNetCore6.Models;
using Microsoft.AspNetCore.Cors;

namespace ApiAspNetCore6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors(origins: "*", headers: "*", methods: "GET,POST,PUT,DELETE,OPTIONS")]
    [EnableCors("MyPolicy")]
    public class ProductController: ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetProduct")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<Product> GetById(int Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product producto)
        {
            _context.Products.Add(producto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = producto.id }, producto);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }

    }
}
