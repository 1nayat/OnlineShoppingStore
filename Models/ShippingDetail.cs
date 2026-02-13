using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingStore.Models;

public partial class ShippingDetail
{
    [Required]
    public int ShippingDetailId { get; set; }
    [Required]
    public int MemberId { get; set; }
    [Required]
    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public int OrderId { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentType { get; set; } = null!;

    public virtual TblMember Member { get; set; } = null!;
}
