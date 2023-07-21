using Microsoft.EntityFrameworkCore;
using SistemasLanche.Context;
using SistemasLanche.Models;

namespace SistemasLanche.Areas.Admin.Services
{
	public class RelatorioVendasService
	{
		private readonly AppDbContext context;

		public RelatorioVendasService(AppDbContext context)
		{
			this.context = context;
		}
		public async Task<List<Pedido>> FindByDatesAsync(DateTime? minDate, DateTime? maxDate) 
		{
			var resultado = from obj in context.Pedidos select obj;
			if (minDate.HasValue) 
			{
				resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
			}
			if (maxDate.HasValue)
			{
				resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
			}

			return await resultado.Include(l => l.PedidoItens)
				.ThenInclude(l => l.Lanche)
				.OrderByDescending(x => x.PedidoEnviado)
				.ToListAsync();
		}
	}
}
