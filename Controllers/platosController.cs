using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AP_650.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba1.Properties
{
    [Route("apiXlk/[controller]")]
    [ApiController]
    public class platosController : ControllerBase{
        private readonly restauranteContext _restauranteContexto;

        public platosController(restauranteContext restauranteContexto){
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Getto(){
            List<platos> listadoPlato = (from e in _restauranteContexto.platos
                                            select e).ToList();
            if (listadoPlato.Count() == 0){
                return NotFound();
            }
            return Ok(listadoPlato);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Getid(int id){
            platos? plato = (from e in _restauranteContexto.platos
                                    where e.platoId == id
                                    select e).FirstOrDefault();
            if (plato == null){
                return NotFound();
            }
            return Ok(plato);
        }
        
        [HttpGet]
        [Route("GetByLess/{cant}")]
        public IActionResult GetCli(decimal cant){
            List<platos> plato = (from e in _restauranteContexto.platos
                                    where e.precio <= cant
                                    select e).ToList();
            if (plato.Count() == 0){
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpGet]
        [Route("GetByMore/{cant}")]
        public IActionResult GetMoto(decimal cant){
            List<platos> plato = (from e in _restauranteContexto.platos
                                    where e.precio >= cant
                                    select e).ToList();
            if (plato.Count() == 0){
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlato([FromBody] platos plat){
            try{
                _restauranteContexto.platos.Add(plat);
                _restauranteContexto.SaveChanges();
                return Ok(plat);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            //Obtener dato actual
            platos? platoActual = (from e in _restauranteContexto.platos
                                    where e.platoId == id
                                    select e).FirstOrDefault();
            //Verificar su existencia
            if (platoActual == null){
                return NotFound();
            }
            //Si se encuentra, modificar
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;
            //Marcando como modificado
            _restauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPlato(int id){
            platos? plato = (from e in _restauranteContexto.platos
                                    where e.platoId == id
                                    select e).FirstOrDefault();
            if(plato == null){
                return NotFound();
            }
            _restauranteContexto.platos.Attach(plato);
            _restauranteContexto.platos.Remove(plato);
            _restauranteContexto.SaveChanges();
            return Ok(plato);
        }
    }
}