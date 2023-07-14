using SistemasLanche.Models;
using SistemasLanche.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository,
            CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedidos = 0;
            decimal precoTotalPedido = 0.0m;
            //Obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItens = items;
            //Verifica se existem itens de pedido
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio que tal incluir um lanche...");
            }
            //Calcular o total de itens e o total do pedido
            foreach (var item in items)
            {
                totalItensPedidos += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);

            }

            //Atribuir os valores obtidos ao pedido

            pedido.TotalItensPedido = totalItensPedidos;
            pedido.PedidoTotal = precoTotalPedido;


            //Valida os dados do pedido
            if(ModelState.IsValid)
            {
                //Criar o pedido e os detalhes do pedido
                _pedidoRepository.CriarPedido(pedido);

                //Define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //Limpa carrinho de compra
                _carrinhoCompra.LimparCarrinho();

                //Exibir a View com dados do cliente e do pedido

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}
