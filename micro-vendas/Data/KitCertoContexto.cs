using micro_vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace micro_vendas.Data
{
    public class KitCertoContexto : DbContext
    {
        public KitCertoContexto(DbContextOptions<KitCertoContexto> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}