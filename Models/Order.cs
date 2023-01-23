using System;
using System.Collections.Generic;

namespace IntegralTradingJSSignalREfcore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int TransactionId { get; set; }

    public virtual ICollection<Hvicsv> Hvicsvs { get; } = new List<Hvicsv>();
}
