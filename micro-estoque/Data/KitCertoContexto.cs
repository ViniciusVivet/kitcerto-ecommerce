using micro_estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace micro_estoque.Data
{
    public class KitCertoContexto : DbContext
    {
        public KitCertoContexto(DbContextOptions<KitCertoContexto> options) : base(options)
        {
        }

        public DbSet<SemiJoia> SemiJoias { get; set; }
    }
}