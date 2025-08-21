using System;
using System.Collections.Generic;

namespace shared_kit.Models
{
    public class PedidoDTO
    {
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemDoPedidoDTO> Itens { get; set; } = new List<ItemDoPedidoDTO>();
    }

    public class ItemDoPedidoDTO
    {
        public Guid ProdutoId { get; set; }
        public string? NomeDoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}