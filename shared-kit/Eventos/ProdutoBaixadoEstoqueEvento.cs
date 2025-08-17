using System;
namespace shared_kit.Eventos
{
    public class ProdutoBaixadoEstoqueEvento : BaseEvento
    {
        public Guid ProdutoId { get; set; }
        public int QuantidadeRetirada { get; set; }
        public ProdutoBaixadoEstoqueEvento(Guid produtoId, int quantidade, string origem) 
            : base(origem)
        {
            ProdutoId = produtoId;
            QuantidadeRetirada = quantidade;
        }
    }
}