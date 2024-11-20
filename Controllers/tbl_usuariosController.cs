using ApiAspNetCore6.Context;
using ApiAspNetCore6.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAspNetCore6.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors(origins: "*", headers: "*", methods: "GET,POST,PUT,DELETE,OPTIONS")]
    [EnableCors("MyPolicy")]
    public class tbl_usuariosController: ControllerBase
    {

        private readonly MyDbContext _context;

        public tbl_usuariosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUsuario")]
        public ActionResult<IEnumerable<tbl_usuario>> GetAll()
        {
            return _context.tbl_usuario.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<tbl_usuario> GetById(int Id)
        {
            var tbl_Usuarios = _context.tbl_usuario.Find(Id);
            if (tbl_Usuarios == null)
            {
                return NotFound();
            }
            return tbl_Usuarios;
        }

        [HttpPost]
        public ActionResult<tbl_usuario> Create(tbl_usuario tbl_Usuario)
        {
            _context.tbl_usuario.Add(tbl_Usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = tbl_Usuario.id }, tbl_Usuario);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, tbl_usuario tbl_Usuario)
        {
            if (id != tbl_Usuario.id)
            {
                return BadRequest();
            }
            _context.Entry(tbl_Usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<tbl_usuario> Delete(int id)
        {
            var tbl_Usuario = _context.tbl_usuario.Find(id);
            if (tbl_Usuario == null)
            {
                return NotFound();
            }
            _context.tbl_usuario.Remove(tbl_Usuario);
            _context.SaveChanges();
            return tbl_Usuario;
        }
    }
}
