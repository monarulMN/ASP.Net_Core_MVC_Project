using System.ComponentModel.DataAnnotations;

namespace U_OnlineBazar.Models
{
    public class PaymentViewModel
    {
        [Display(Name ="Order No")]
        public string OrderNo { get; set; }
        [Required]
        [Display(Name ="Card Number")]
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
