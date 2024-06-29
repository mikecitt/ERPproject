using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class SiteUser : Entity
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int Role { get; set; }

    public virtual ICollection<ShopOrder> ShopOrders { get; } = new List<ShopOrder>();
}
