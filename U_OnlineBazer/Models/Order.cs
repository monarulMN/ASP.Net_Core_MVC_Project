using System.ComponentModel.DataAnnotations;

namespace U_OnlineBazer.Models
{
    public class Order
    {
        public Order() 
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }
        [Display(Name="Order No")]
        public string OrderNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="phone No")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
       
        //public int OrderId { get; set; }
        //public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
       
        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
