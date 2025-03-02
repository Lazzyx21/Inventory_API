using Inventory_API.Models;
using Inventory_API.Services.Interface;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace Inventory_API.Services
{
    //Provides insights into stock movement, sales trends, and inventory value.


    public class InvReporting_AnalyticsServices : IInvReporting_AnalyticsServices
    {
        //Features:
        //    Stock Movement Reports: Track incoming and outgoing inventory
        //    Dead Stock Report: Identify slow-moving items
        //    Stock Valuation Report: FIFO/LIFO inventory value calculations
        //    Sales vs Inventory Report: Helps forecast demand

    }
}
