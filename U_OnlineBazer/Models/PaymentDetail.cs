using System.ComponentModel.DataAnnotations;

namespace U_OnlineBazar.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        [Key]
        public int PaymentId { get; set; }
        //public int OrderId { get; set; }
        [Display(Name ="Order No")]
        public string OrderNo { get; set; }
        [Required]
        [Display(Name ="Card Number")]
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
