using Microsoft.AspNetCore.Mvc;
using SistemasLanche.Models;
using SistemasLanche.Repositories.Interfaces;
using SistemasLanche.ViewModels;

namespace SistemasLanche.Controllers
{
	public class CarrinhoCompraController : Controller
	{
		private readonly ILancheRepository _lancheRepository;
		private readonly CarrinhoCompra _carrinhoCompra;

		public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
		{
			_lancheRepository = lancheRepository;
			_carrinhoCompra = carrinhoCompra;
		}

		public IActionResult Index()
		{
			var itens = _carrinhoCompra.GetCarrinhoCompraItems();
			_carrinhoCompra.CarrinhoCompraItens = itens;
			var carrinhoCompraVM = new CarrinhoCompraViewModel
			{
				CarrinhoCompra = _carrinhoCompra,
				CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
			};
			return View(carrinhoCompraVM);
		}
		public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
		{
			var lancheSelecionado = _lancheRepository.Lanches
				.FirstOrDefault(p => p.LancheId == lancheId);
			if(lancheSelecionado != null)
			{
				_carrinhoCompra.adicionarAoCarrinho(lancheSelecionado);
			}
			return RedirectToAction("Index");
		}
		public  IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
		{
			var lancheSelecionado = _lancheRepository.Lanches
				.FirstOrDefault(p => p.LancheId == lancheId);
			if (lancheSelecionado != null)
			{
				_carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
			}
			return RedirectToAction("Index");
		}
	}
}
