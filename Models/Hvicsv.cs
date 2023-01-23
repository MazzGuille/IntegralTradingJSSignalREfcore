using System;
using System.Collections.Generic;

namespace IntegralTradingJSSignalREfcore.Models;

public partial class Hvicsv
{
    public int Id { get; set; }

    public decimal? Uhml { get; set; }

    public decimal? Ui { get; set; }

    public decimal? Strength { get; set; }

    public decimal? Sfi { get; set; }

    public decimal? Mic { get; set; }

    public string ColorGrade { get; set; }

    public decimal? TrashId { get; set; }

    public int? OrderId { get; set; }

    public virtual Order Order { get; set; }
}
