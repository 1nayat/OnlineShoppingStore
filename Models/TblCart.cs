using System;
using System.Collections.Generic;

namespace OnlineShoppingStore.Models;

public partial class TblCart
{
    public int CartId { get; set; }

    public int? ProductId { get; set; }

    public int? MenberId { get; set; }

    public int? CartStatuesId { get; set; }

    public virtual TblProduct? Product { get; set; }
}
