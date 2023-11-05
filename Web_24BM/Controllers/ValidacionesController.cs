using Microsoft.AspNetCore.Mvc;
using Web_24BM.Models;
using Web_24BM.Services;

namespace Web_24BM.Controllers
{
    public class ValidacionesController : Controller
    {

        private readonly ContactoService contactoService;
        public ValidacionesController(ContactoService contactoService)
        {
            this.contactoService = contactoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Curriculum()
        {
            return View();
        }

        public IActionResult ListadoCurriculum()
        {
            var datos = this.contactoService.getCurriculums();
            return View("ListadoCurriculum" , datos);
        }

        [HttpPost]

        public IActionResult EliminarCurriculum(int id)
        {
            var Eliminado = this.contactoService.eliminarCurriculum(id);
            var datos = this.contactoService.getCurriculums();
            TempData["Eliminado"] = Eliminado;
            return RedirectToAction("ListadoCurriculum");
        }

        [HttpPost]

        public IActionResult ActualizarCurriculum(int id)
        {
            Contacto model = this.contactoService.obtenerCurriculumPorId(id);
            Curriculum model2 = new Curriculum()
            {
                Apellidos = model.Apellidos,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Email = model.Email,
                FechaNacimiento = (DateTime)model.FechaNacimiento,
                Objetivo = model.Objetivo,

            };
            return View("ActualizarCurriculum", model);
        }



        [HttpPost]
        public IActionResult ActualizarContactoFinal(Contacto model)
        {

            if (!ModelState.IsValid)
            {
                return View("Curriculum", model);
            }

            string mensaje = " ";

            mensaje = "Datos Correctos";

            this.contactoService.ActualizarContacto(model);

            TempData["Completado"] = "Datos guardados correctamente";

            TempData["Actualizado"] = true;

            return RedirectToAction("ListadoCurriculum");

        }



        [HttpPost]
        public IActionResult EnviarFormulario(Curriculum model)
        {

            if (!ModelState.IsValid)
            {
                return View("Curriculum", model);
            }

            string mensaje = " ";
            
            mensaje = "Datos Correctos";
            
            this.contactoService.CrearContacto(model);
            
            TempData["Completado"] = "Datos guardados correctamente";

            return View("Curriculum",model) ;
            
        }

    }
}

