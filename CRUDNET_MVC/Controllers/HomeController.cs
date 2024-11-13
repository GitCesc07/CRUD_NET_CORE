using CRUDNET_MVC.Data;
using CRUDNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRUDNET_MVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly DBContext _dBContext;

        // Obteniendo la información de la base de datos
        public HomeController(DBContext contexto)
        {
            _dBContext = contexto;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dBContext.Contactos.ToListAsync());
        }

        // Método para crear los contactos
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contactos contacto)
        {
            if (ModelState.IsValid)
            {
                // Agregar fecha de creación según la información que manda la pc
                contacto.FechaCreacion = DateTime.Now;

                _dBContext.Contactos.Add(contacto);
                await _dBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Método para editar los datos del contacto
        [HttpGet]
        public IActionResult Editar(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var contacto = _dBContext.Contactos.Find(Id);

            if(contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contactos contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.FechaCreacion = DateTime.Now;
                _dBContext.Update(contacto);
                await _dBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Método para ver los datos del contacto
        [HttpGet]
        public IActionResult Detalle(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var contacto = _dBContext.Contactos.Find(Id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // Método para eliminar los datos del contacto
        [HttpGet]
        public IActionResult Borrar(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var contacto = _dBContext.Contactos.Find(Id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var contacto = await _dBContext.Contactos.FindAsync(Id);
            if (contacto == null)
            {
                return View();
            }

            // Eliminar si existe
            _dBContext.Contactos.Remove(contacto);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
