using System;
using System.Collections.Generic;

namespace OnlineShoppingStore.Models;

public partial class TblCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
