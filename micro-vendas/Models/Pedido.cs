using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace micro_vendas.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataDoPedido { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorTotal { get; set; }
        public List<ItemDoPedido> Itens { get; set; } = new List<ItemDoPedido>();
    }

    public class ItemDoPedido
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string NomeDoProduto { get; set; }
        public int Quantidade { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoUnitario { get; set; }
    }
}