using System;
using System.Collections.Generic;

namespace TrgovinskaRadnja.Data.Model;

public partial class Stock : Entity
{

    public int StockQuantity { get; set; }

    public int WarehouseId { get; set; }

    public virtual Warehouse Warehouse { get; set; }
}
