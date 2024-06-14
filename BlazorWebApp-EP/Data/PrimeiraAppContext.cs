using BlazorWebApp_EP.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApp_EP.Data
{
    public class PrimeiraAppContext : DbContext
    {
        public PrimeiraAppContext (DbContextOptions<PrimeiraAppContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; } = default!;
    }
}
