using System;
using System.Collections.Generic;

namespace OnlineShoppingStore.Models;

public partial class TableCartStatus
{
    public int CartStatusId { get; set; }

    public string CartStatus { get; set; } = null!;
}
