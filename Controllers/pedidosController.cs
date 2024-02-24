using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AP_650.Models;
using Microsoft.EntityFrameworkCore;

namespace Prueba1.Properties
{
    [Route("apiXlk/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase{
        private readonly restauranteContext _restauranteContexto;

        public pedidosController(restauranteContext restauranteContexto){
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Getto(){
            List<pedidos> listadoPedido = (from e in _restauranteContexto.pedidos
                                            select e).ToList();
            if (listadoPedido.Count() == 0){
                return NotFound();
            }
            return Ok(listadoPedido);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Getid(int id){
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                                    where e.pedidoId == id
                                    select e).FirstOrDefault();
            if (pedido == null){
                return NotFound();
            }
            return Ok(pedido);
        }
        
        [HttpGet]
        [Route("GetByCliente/{id}")]
        public IActionResult GetCli(int id){
            List<pedidos> pedido = (from e in _restauranteContexto.pedidos
                                    where e.clienteId == id
                                    select e).ToList();
            if (pedido.Count() == 0){
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetByMoto/{id}")]
        public IActionResult GetMoto(int id){
            List<pedidos> pedido = (from e in _restauranteContexto.pedidos
                                    where e.motoristaId == id
                                    select e).ToList();
            if (pedido.Count() == 0){
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] pedidos pedi){
            try{
                _restauranteContexto.pedidos.Add(pedi);
                _restauranteContexto.SaveChanges();
                return Ok(pedi);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            //Obtener dato actual
            pedidos? pedidoActual = (from e in _restauranteContexto.pedidos
                                    where e.pedidoId == id
                                    select e).FirstOrDefault();
            //Verificar su existencia
            if (pedidoActual == null){
                return NotFound();
            }
            //Si se encuentra, modificar
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;
            //Marcando como modificado
            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPedido(int id){
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                                    where e.pedidoId == id
                                    select e).FirstOrDefault();
            if(pedido == null){
                return NotFound();
            }
            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();
            return Ok(pedido);
        }
    }
}