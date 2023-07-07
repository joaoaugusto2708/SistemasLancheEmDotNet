using SistemasLanche.Models;

namespace SistemasLanche.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
