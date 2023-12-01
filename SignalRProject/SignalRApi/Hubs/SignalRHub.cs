using Microsoft.AspNetCore.SignalR;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
	public class SignalRHub : Hub
	{
		SignalRContext context = new SignalRContext();
		public async Task SendCategoryCount()
		{
			var value = context.Categories.Count();
			await Clients.All.SendAsync("ReceiveCategoryCount", value);
		}
		public async Task SendProductCount()
		{
			var value2 = context.Products.Count();
			await Clients.All.SendAsync("ReceiveProductCount", value2);
		}
	}
}
