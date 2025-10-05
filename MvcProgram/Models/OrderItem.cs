using System;
using System.Collections.Generic;

namespace MvcProgram.Models;

public partial class OrderItem
{
    public Guid OrderItemId { get; set; }

    public Guid ExchangeId { get; set; }

    public Guid OrderId { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
