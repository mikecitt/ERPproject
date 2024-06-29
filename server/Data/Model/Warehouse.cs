using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class Warehouse : Entity
{

    public int Capacity { get; set; }

    public string Location { get; set; }


    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();
}
