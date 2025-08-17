using System.ComponentModel.DataAnnotations.Schema;

namespace micro_estoque.Models
{
    public class SemiJoia
    {
        public Guid Id { get; set; }
        public string NomeDaJoia { get; set; }
        public string Descricao { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }
        
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataDeCadastro { get; set; }
    }
}