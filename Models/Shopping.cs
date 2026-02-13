using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.Models
{
    public class ShippingDetails
    {
     
        public int ShippingDetailId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public int OrderId { get; set; }

        public decimal AmountPaid { get; set; }
        [Required]
        public string PaymentType { get; set; } = null!;
    }
}
