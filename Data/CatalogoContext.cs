using Microsoft.EntityFrameworkCore;
using CatalogoJogosAPI.Modelos;

namespace CatalogoJogosAPI.Data
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Jogo> Jogos { get; set; }

    }
}
