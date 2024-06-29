using System;
using System.Collections.Generic;

namespace TrgovinskaRadnja.Data.Model;

public partial class Category : Entity
{

    public string CategoryName { get; set; }


    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
