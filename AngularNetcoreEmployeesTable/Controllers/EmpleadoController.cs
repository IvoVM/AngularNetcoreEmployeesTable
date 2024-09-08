using AngularNetcoreEmployeesTable.Data;
using AngularNetcoreEmployeesTable.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetcoreEmployeesTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;

        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Empleado> Lista = await _empleadoData.GetAll();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Empleado empleado = await _empleadoData.GetById(id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empleado empleadoData)
        {
            bool answer = await _empleadoData.Post(empleadoData);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = answer });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Empleado empleadoData)
        {
            bool answer = await _empleadoData.Put(empleadoData);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = answer });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            bool answer = await _empleadoData.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = answer });
        }
    }
}