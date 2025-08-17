using micro_estoque.Data;
using micro_estoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace micro_estoque.Controllers
{
    [ApiController]
    [Route("api/semijoias")]
    public class SemiJoiasController : ControllerBase
    {
        private readonly KitCertoContexto _contexto;

        public SemiJoiasController(KitCertoContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/semijoias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemiJoia>>> GetSemiJoias()
        {
            return await _contexto.SemiJoias.ToListAsync();
        }

        // POST: api/semijoias
        [HttpPost]
        public async Task<ActionResult<SemiJoia>> PostSemiJoia(SemiJoia semiJoia)
        {
            semiJoia.Id = Guid.NewGuid();
            semiJoia.DataDeCadastro = DateTime.UtcNow;

            _contexto.SemiJoias.Add(semiJoia);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSemiJoias), new { id = semiJoia.Id }, semiJoia);
        }

        // PUT: api/semijoias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSemiJoia(Guid id, SemiJoia semiJoia)
        {
            if (id != semiJoia.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(semiJoia).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemiJoiaExists(id))
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

        // DELETE: api/semijoias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemiJoia(Guid id)
        {
            var semiJoia = await _contexto.SemiJoias.FindAsync(id);
            if (semiJoia == null)
            {
                return NotFound();
            }

            _contexto.SemiJoias.Remove(semiJoia);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool SemiJoiaExists(Guid id)
        {
            return _contexto.SemiJoias.Any(e => e.Id == id);
        }
    }
}