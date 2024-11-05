using Microsoft.AspNetCore.Mvc;
using Stripe;
using U_OnlineBazar.Models;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;
using U_OnlineBazer.Utility;

namespace U_OnlineBazer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;


        public OrderController(ApplicationDbContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
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
            List<Stripe.Product> products = HttpContext.Session.Get<List<Stripe.Product>>("products");
            if(products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = int.Parse( product.Id);
                    anOrder.OrderDetails.Add(orderDetails);

                }
            }
            anOrder.OrderNo = GetOrderNo();
            _dbContext.Orders.Add(anOrder);
            await _dbContext.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Stripe.Product>());

            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _dbContext.Orders.ToList().Count()+1;
            return rowCount.ToString("000");
        }

        public async Task<IActionResult> ProcessPayment()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel payment)
        {
            // Initialize Stripe client
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new ChargeCreateOptions
            {
                Amount = 5000, // Amount in cents (e.g., $50.00)
                Currency = "usd",
                Source = payment.CardNumber, // Token from the payment form
                Description = "Order Payment"
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Status == "succeeded")
            {
                // Payment successful, process the order
                return RedirectToAction("Success");
            }
            else
            {
                // Payment failed, return an error message
                ModelState.AddModelError("", "Payment failed. Please try again.");
                return View("Payment");
            }


        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
