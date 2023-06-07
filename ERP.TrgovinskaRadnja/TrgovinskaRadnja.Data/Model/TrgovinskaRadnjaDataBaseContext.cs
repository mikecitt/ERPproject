using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrgovinskaRadnja.Data.Model;

public partial class TrgovinskaRadnjaDataBaseContext : DbContext
{
    public TrgovinskaRadnjaDataBaseContext()
    {
    }

    public TrgovinskaRadnjaDataBaseContext(DbContextOptions<TrgovinskaRadnjaDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShopOrder> ShopOrders { get; set; }

    public virtual DbSet<SiteUser> SiteUsers { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CL-SL\\MSSQL2022;Initial Catalog=ERPbaza;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F51E910E7");

            entity.ToTable("Cart");


            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.BuyerId).IsRequired()
                .HasMaxLength(200)
                .HasColumnName("buyerId");

            entity.Property(e => e.PaymentIntentId).HasColumnName("PaymentIntentId").HasMaxLength(255);
            entity.Property(e => e.ClientSecret).HasColumnName("ClientSecret").HasMaxLength(255);



        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3213E83FA7AEFF70");

            entity.ToTable("CartItem");

            entity.HasIndex(e => e.ProductId, "UQ__CartItem__2D10D14B00FDF95A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cartID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.Product).WithOne(p => p.CartItem)
                .HasForeignKey<CartItem>(d => d.ProductId)
                .HasConstraintName("FK_CartItem_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F4A80FE93");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("category_name");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3213E83FD50AD131");

            entity.ToTable("OrderItem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83FE0847D41");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("image_path");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("product_name");
            entity.Property(e => e.StockStatus).HasColumnName("stock_status");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ShopOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShopOrde__3213E83F9126693F");

            entity.ToTable("ShopOrder");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveredDate)
                .HasColumnType("datetime")
                .HasColumnName("delivered_date");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("total");



            entity.Property(e => e.BuyerId).HasColumnName("BuyerId").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_FullName).HasColumnName("ShippingAddress_FullName").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_Address1).HasColumnName("ShippingAddress_Address1").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_Address2).HasColumnName("ShippingAddress_Address2").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_City).HasColumnName("ShippingAddress_City").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_State).HasColumnName("ShippingAddress_State").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_Zip).HasColumnName("ShippingAddress_Zip").HasMaxLength(200);
            entity.Property(e => e.ShippingAddress_Country).HasColumnName("ShippingAddress_Country").HasMaxLength(200);


            entity.Property(e => e.DeliveryFee).HasColumnName("DeliveryFee").IsRequired();
            entity.Property(e => e.OrderStatus).HasColumnName("OrderStatus").IsRequired();



            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.ShopOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<SiteUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SiteUser__3213E83F6ED5B364");

            entity.ToTable("SiteUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<SiteUser>(entry =>
        {
            entry.ToTable("SiteUser", tb => tb.HasTrigger("trg_CreateCartOnUserInsert"));
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stock__3213E83F70895808");

            entity.ToTable("Stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouseID");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Warehouse");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Warehous__3213E83FF12507B4");

            entity.ToTable("Warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("location");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
