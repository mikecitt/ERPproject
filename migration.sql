-- Create a new database
CREATE DATABASE baza;
GO

-- Use the new database
USE baza;
GO

CREATE TABLE [dbo].[Warehouse](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[capacity] [int] NOT NULL,
	[location] [nvarchar](100) NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[Stock](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stock_quantity] [int] NOT NULL,
	[warehouseID] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Warehouse] FOREIGN KEY([warehouseID])
REFERENCES [dbo].[Warehouse] ([id])
GO

ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Warehouse]
GO



CREATE TABLE [dbo].[SiteUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](30) NOT NULL,
	[last_name] [nvarchar](40) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](250) NOT NULL,
	[phone] [nvarchar](30) NOT NULL,
	[email] [nvarchar](30) NOT NULL,
	[role] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ShopOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[order_date] [datetime] NOT NULL,
	[delivered_date] [datetime] NOT NULL,
	[total] [numeric](10, 2) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[BuyerId] [nvarchar](200) NULL,
	[ShippingAddress_FullName] [nvarchar](200) NULL,
	[ShippingAddress_Address1] [nvarchar](200) NULL,
	[ShippingAddress_City] [nvarchar](200) NULL,
	[ShippingAddress_Address2] [nvarchar](200) NULL,
	[ShippingAddress_State] [nvarchar](200) NULL,
	[ShippingAddress_Zip] [nvarchar](200) NULL,
	[ShippingAddress_Country] [nvarchar](200) NULL,
	[DeliveryFee] [int] NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[PaymentIntentId] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ShopOrder]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[SiteUser] ([id])
GO

ALTER TABLE [dbo].[ShopOrder] CHECK CONSTRAINT [FK_Orders_Users]
GO


CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](30) NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_name] [nvarchar](100) NOT NULL,
	[image_path] [nvarchar](200) NOT NULL,
	[description] [varchar](200) NOT NULL,
	[price] [numeric](10, 2) NOT NULL,
	[stock_status] [bit] NULL,
	[categoryID] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[QuantityInStock] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD CONSTRAINT [FK_Product_Category] FOREIGN KEY([categoryID])
REFERENCES [dbo].[Category] ([id])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO



CREATE TABLE [dbo].[OrderItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderID] [int] NOT NULL,
	[productID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[cost] [numeric](10, 2) NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Orders] FOREIGN KEY([orderID])
REFERENCES [dbo].[ShopOrder] ([id])
GO

ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Orders]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([id])
GO

ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO


CREATE TABLE [dbo].[Cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[buyerId] [nvarchar](200) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[PaymentIntentId] [varchar](255) NULL,
	[ClientSecret] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[CartItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cartID] [int] NOT NULL,
	[productID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY([cartID])
REFERENCES [dbo].[Cart] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Cart]
GO

ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Product] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Product]
GO



