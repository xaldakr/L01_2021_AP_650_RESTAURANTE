using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AP_650.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba1.Properties
{
    [Route("apiXlk/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase{
        private readonly restauranteContext _restauranteContexto;

        public motoristasController(restauranteContext restauranteContexto){
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Getto(){
            List<motoristas> listadoMotorista = (from e in _restauranteContexto.motoristas
                                            select e).ToList();
            if (listadoMotorista.Count() == 0){
                return NotFound();
            }
            return Ok(listadoMotorista);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Getid(int id){
            motoristas? motorista = (from e in _restauranteContexto.motoristas
                                    where e.motoristaId == id
                                    select e).FirstOrDefault();
            if (motorista == null){
                return NotFound();
            }
            return Ok(motorista);
        }
        
        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetCli(string name){
            List<motoristas> motorista = (from e in _restauranteContexto.motoristas
                                    where e.nombreMotorista.Contains(name)
                                    select e).ToList();
            if (motorista.Count() == 0){
                return NotFound();
            }
            return Ok(motorista);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarMotorista([FromBody] motoristas pedi){
            try{
                _restauranteContexto.motoristas.Add(pedi);
                _restauranteContexto.SaveChanges();
                return Ok(pedi);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] motoristas motoristaModificar)
        {
            //Obtener dato actual
            motoristas? motoristaActual = (from e in _restauranteContexto.motoristas
                                    where e.motoristaId == id
                                    select e).FirstOrDefault();
            //Verificar su existencia
            if (motoristaActual == null){
                return NotFound();
            }
            //Si se encuentra, modificar
            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;
            //Marcando como modificado
            _restauranteContexto.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(motoristaModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarMotorista(int id){
            motoristas? motorista = (from e in _restauranteContexto.motoristas
                                    where e.motoristaId == id
                                    select e).FirstOrDefault();
            if(motorista == null){
                return NotFound();
            }
            _restauranteContexto.motoristas.Attach(motorista);
            _restauranteContexto.motoristas.Remove(motorista);
            _restauranteContexto.SaveChanges();
            return Ok(motorista);
        }
    }
}