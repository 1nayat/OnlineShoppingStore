using System;
using System.Collections.Generic;

namespace OnlineShoppingStore.Models;

public partial class TblMember
{
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ShippingDetail> ShippingDetails { get; set; } = new List<ShippingDetail>();
}
