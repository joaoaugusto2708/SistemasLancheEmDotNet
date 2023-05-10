using SistemasLanche.Context;
using SistemasLanche.Models;
using SistemasLanche.Repositories.Interfaces;

namespace SistemasLanche.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias; //Retorna todas categorias
    }
}
