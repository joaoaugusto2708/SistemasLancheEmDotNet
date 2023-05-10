using Microsoft.EntityFrameworkCore;
using SistemasLanche.Context;
using SistemasLanche.Models;
using SistemasLanche.Repositories.Interfaces;

namespace SistemasLanche.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c=> c.Categoria);

        public IEnumerable<Lanche> LanchesPreferiodos => _context.Lanches.
            Where(l => l.IsLanchePreferido).
            Include(c => c.Categoria); // Tras todos os lanches favoritos e sus categorias


        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId); // Retorna um lanche especifico pelo ID
        }
    }
}
