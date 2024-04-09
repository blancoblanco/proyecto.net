using Gestion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ProductosContext _context;

        public UsuariosController(ProductosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crearU")]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("listaU")]
        public async Task<ActionResult<IEnumerable<Usuario>>>listarUsuarios(){
           
           var usuarios =await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }
        [HttpGet]

        [Route("verUsuario")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            //obtener el producto de la base de datos
            Usuario usuario = await _context.Usuarios.FindAsync(id);

            //devolver el producto
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }


        [HttpPut]
        [Route("editarU")]
        public async Task<IActionResult> ActualizarUsuario(int id ,Usuario usuario) { 
        
          var usuarioExistente=await _context.Usuarios.FindAsync(id);

            usuarioExistente!.nombre=usuario.nombre;
            usuarioExistente!.correo = usuario.correo;
            usuarioExistente!.telefono = usuario.telefono;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminarU")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuarioBorrado = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarioBorrado);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
