using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using proyecto.Models;
using System.Text;

namespace proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7092/api");
        }
        public async Task<IActionResult> IndexUsuarios()
        {
            var response = await _httpClient.GetAsync("/api/Usuarios/listaU");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(content);
                return View("IndexUsuarios", usuarios);
            }

            // Manejar el caso en que la solicitud HTTP no fue exitosa.
            return View(new List<UsuarioViewModel>()); // Puedes mostrar una vista vacía o un mensaje de error.
        }



        public IActionResult CreateUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuarioViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Usuarios/crearU", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("IndexUsuarios");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario.");
                }
            }
            return View(usuarioViewModel);
        }

        public async Task<IActionResult> EditUsuario(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Usuarios/verUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                // Devuelve la vista de edición con los detalles del producto.
                return View(usuario);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del producto.
                return RedirectToAction("DetailsUsuario"); // Redirige a la página de lista de productos u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUsuario(int id,UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(usuarioViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Usuarios/editarU?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del producto.
                    return RedirectToAction("IndexUsuarios", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el usuario.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(usuarioViewModel);
        }

        public async Task<IActionResult> DetailsUsuario(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Usuarios/verUsuario?id={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(content);

                // Devuelve la vista de edición con los detalles del producto.
                return View(usuario);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del producto.
                return RedirectToAction("DetailsUsuario"); // Redirige a la página de lista de productos u otra acción apropiada.
            }
        }
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Usuarios/eliminarU?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de productos.
                return RedirectToAction("IndexUsuarios");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el Usuario.";
                return RedirectToAction("IndexUsuarios");
            }
        }

    }
}
