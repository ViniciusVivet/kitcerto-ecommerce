using Microsoft.AspNetCore.Mvc;
using micro_estoque.ServicosMensageria;
using shared_kit.Eventos;
using System;

namespace micro_estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitCertoEstoqueController : ControllerBase
    {
        [HttpPost("baixar")]
        public IActionResult BaixarEstoque(Guid produtoId, int quantidade)
        {
            var produtor = new RabbitMqProdutor();
            var evento = new ProdutoBaixadoEstoqueEvento(produtoId, quantidade, "MicroEstoque");
            produtor.EnviarMensagem(evento);
            Console.WriteLine($"[KITCERTO-LOG] Evento de baixa de estoque enviado para o produto {produtoId}.");
            return Ok("Evento enviado com sucesso!");
        }
    }
}