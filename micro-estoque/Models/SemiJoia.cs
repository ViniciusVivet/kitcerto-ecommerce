using System.ComponentModel.DataAnnotations.Schema;

namespace micro_estoque.Models
{
    public class SemiJoia
    {
        public Guid Id { get; set; }

        // Inicializa para evitar CS8618
        public string NomeDaJoia { get; set; } = string.Empty;
        public string Descricao  { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        public int QuantidadeEmEstoque { get; set; }

        // Define um valor padrão ao criar
        public DateTime DataDeCadastro { get; set; } = DateTime.UtcNow;
    }
}
