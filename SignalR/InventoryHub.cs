using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
namespace Inventory_API.SignalR
{
    public class InventoryHub : Hub
    {
        public async Task SendInvList(object product)
        {
            await Clients.All.SendAsync("Send List of products", product);
        }
        public async Task SendCreateInv(object Cproduct)
        {
            await Clients.All.SendAsync("Data Created", Cproduct);
        }
        public async Task SendUpdateInv(object Uproduct)
        {
            await Clients.All.SendAsync("Data Updated", Uproduct);
        }
        public async Task SendDeleteInvPrd(int IproductId)
        {
            await Clients.All.SendAsync("Data has been Deleted", IproductId);
        }
        public async Task SendVerifyInvPrd(int days)
        {
            await Clients.All.SendAsync("Verifying Expired Product", days);
        }
        public async Task SendInvCat(string catName)
        {
            await Clients.All.SendAsync("Get Product present in inventory accorinding to Names", catName);
        }
    }
}
