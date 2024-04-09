using Gestion.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosContext _context;

        public ProductosController(ProductosContext context)
        {
            _context = context;
        }



        [HttpPost]
        [Route("crearP")]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("listaP")]
        public async Task<ActionResult<IEnumerable<Producto>>> listarProductos()
        {

            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpGet]
        [Route("verProducto")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //obtener el producto de la base de datos
            Producto producto = await _context.Productos.FindAsync(id);

            //devolver el producto
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }


        [HttpPut]
        [Route("editarP")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {

            var productoExistente = await _context.Productos.FindAsync(id);

            productoExistente!.nombre = producto.nombre;
            productoExistente.descripcion = producto.descripcion;
            productoExistente.precio = producto.precio;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminarP")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var productoBorrado = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productoBorrado);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}