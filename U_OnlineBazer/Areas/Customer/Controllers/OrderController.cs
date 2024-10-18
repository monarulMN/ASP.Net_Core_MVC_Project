using Microsoft.AspNetCore.Mvc;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;
using U_OnlineBazer.Utility;

namespace U_OnlineBazer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET Checkout Action Method
        public IActionResult Checkout()
        {
            return View();
        }

        //POST Checkout Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if(products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);

                }
            }
            anOrder.OrderNo = GetOrderNo();
            _dbContext.Orders.Add(anOrder);
            await _dbContext.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Product>());

            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _dbContext.Orders.ToList().Count()+1;
            return rowCount.ToString("000");
        }
    }
}
