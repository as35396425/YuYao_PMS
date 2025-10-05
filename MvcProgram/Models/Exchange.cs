using System;
using System.Collections.Generic;

namespace MvcProgram.Models;

public partial class Exchange
{
    public Guid ExchangeId { get; set; }

    public int? ReqestId { get; set; }

    public int? ResponseId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product? Reqest { get; set; }

    public virtual Product? Response { get; set; }
}
