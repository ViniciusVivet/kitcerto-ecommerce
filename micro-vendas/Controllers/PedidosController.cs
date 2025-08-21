using micro_vendas.Data;
using micro_vendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shared_kit.Models;

namespace micro_vendas.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly KitCertoContexto _contexto;

        public PedidosController(KitCertoContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _contexto.Pedidos.Include(p => p.Itens).ToListAsync();
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido([FromBody] PedidoDTO pedidoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                ClienteId = pedidoDTO.ClienteId,
                DataDoPedido = DateTime.UtcNow,
                ValorTotal = pedidoDTO.ValorTotal,
                Itens = pedidoDTO.Itens.Select(itemDTO => new ItemDoPedido
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = itemDTO.ProdutoId,
                    NomeDoProduto = itemDTO.NomeDoProduto,
                    Quantidade = itemDTO.Quantidade,
                    PrecoUnitario = itemDTO.PrecoUnitario
                }).ToList()
            };

            _contexto.Pedidos.Add(pedido);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidos), new { id = pedido.Id }, pedido);
        }

        // GET: api/pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _contexto.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/pedidos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/pedidos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var pedido = await _contexto.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _contexto.Pedidos.Remove(pedido);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(Guid id)
        {
            return _contexto.Pedidos.Any(e => e.Id == id);
        }
    }
}