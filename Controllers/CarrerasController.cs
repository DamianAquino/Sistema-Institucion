using Microsoft.AspNetCore.Mvc;

namespace API_Institucion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrerasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get_Carreras()
        {
            var carreras = new List<string> { "Ingeniería", "Medicina", "Derecho" };
            return Ok(carreras);
        }

        [HttpGet("{id}")]
        public IActionResult Get_ID(int id)
        {
            Console.WriteLine(id);
            var carrera = new { Id = id, Nombre = "Ingeniería", Duracion = 5, Director = "Tal", Comisiones = "comisiones" };

            return Ok(carrera);
        }
    }
}
