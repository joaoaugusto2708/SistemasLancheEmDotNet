using Microsoft.EntityFrameworkCore;
using SistemasLanche.Context;

namespace SistemasLanche.Models
{
	public class CarrinhoCompra
	{
		private readonly AppDbContext _context;
		//Injeta o contexto no construtor
		public CarrinhoCompra(AppDbContext context)
		{
			_context = context;
		}

		public string CarrinhoCompraId { get; set; }
		public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
		public static CarrinhoCompra GetCarrinho(IServiceProvider services)
		{
			//Define uma sessão
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			//Obtem um serviço do tipo do nosso contexto
			var context = services.GetService<AppDbContext>();
			//Obtem ou gera o Id do carrinho
			string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
			//Atribui o id do carrinho na Sessão
			session.SetString("CarrinhoId", carrinhoId);
			//Retorna o carrinho com o contexto e o Id Atribuido ou obtido
			return new CarrinhoCompra(context)
			{
				CarrinhoCompraId = carrinhoId
			};
		}
		public void adicionarAoCarrinho(Lanche lanche)
		{
			//Verifica a session e retorna null ou item que ja existe
			var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
				s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);
			if (carrinhoCompraItem == null)
			{
				carrinhoCompraItem = new CarrinhoCompraItem
				{
					CarrinhoCompraId = CarrinhoCompraId,
					Lanche = lanche,
					Quantidade = 1
				};
				_context.CarrinhoCompraItems.Add(carrinhoCompraItem);
			}
			else
			{
				carrinhoCompraItem.Quantidade++;
			}
			//Atualiza o item ou salva um novo item
			_context.SaveChanges();
		}
		public int RemoverDoCarrinho(Lanche lanche)
		{
			var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
				s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);
			var quantidadeLocal = 0;
			if (carrinhoCompraItem == null)
			{
				if (carrinhoCompraItem.Quantidade > 1)
				{
					carrinhoCompraItem.Quantidade--;
					quantidadeLocal = carrinhoCompraItem.Quantidade;
				}
				else
				{
					_context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
				}
			}
			_context.SaveChanges();
			return quantidadeLocal;

		}
		public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
		{
			return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItems.
				Where(C => C.CarrinhoCompraId == CarrinhoCompraId)
				.Include(s => s.Lanche)
				.ToList());
		}
		public void LimparCarrinho()
		{
			var carrinhoItens = _context.CarrinhoCompraItems.
				Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
			_context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
			_context.SaveChanges();
		}

		public decimal GetCarrinhoCompraTotal()
		{
			var total = _context.CarrinhoCompraItems.
				Where(c => c.CarrinhoCompraId == CarrinhoCompraId).
				Select(c => c.Lanche.Preco * c.Quantidade).Sum();
			return total;
		}

	}
}
