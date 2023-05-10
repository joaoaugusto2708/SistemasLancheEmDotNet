using Microsoft.EntityFrameworkCore;
using SistemasLanche.Models;

namespace SistemasLanche.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //classe base é DbContext
        {
            
        }
        //Definir as classes DbSet a baixo
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
    }
}
