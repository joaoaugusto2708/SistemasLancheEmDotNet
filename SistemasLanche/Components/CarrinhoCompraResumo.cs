using Microsoft.AspNetCore.Mvc;
using SistemasLanche.Models;
using SistemasLanche.ViewModels;

namespace SistemasLanche.Components
{
	public class CarrinhoCompraResumo : ViewComponent
	{
		private readonly CarrinhoCompra _carrinhoCompra;

		public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
		{
			_carrinhoCompra = carrinhoCompra;
		}

		public IViewComponentResult Invoke()
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
	}
}
