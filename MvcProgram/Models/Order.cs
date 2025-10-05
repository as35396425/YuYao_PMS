using System;
using System.Collections.Generic;

namespace MvcProgram.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public string? ReqestId { get; set; }

    public string? ResponseId { get; set; }

    public int Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual AspNetUser? Reqest { get; set; }

    public virtual AspNetUser? Response { get; set; }
}
