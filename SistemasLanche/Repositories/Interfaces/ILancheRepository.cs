using SistemasLanche.Models;

namespace SistemasLanche.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche>  LanchesPreferiodos { get; }
        Lanche GetLancheById(int lancheId);

    }
}
