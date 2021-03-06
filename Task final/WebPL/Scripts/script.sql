USE [master]
GO

/****** Object:  Database [orderservice]    Script Date: 08.10.2019 3:06:02 ******/
CREATE DATABASE [orderservice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'orderservice', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\orderservice.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'orderservice_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\orderservice_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [orderservice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [orderservice] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [orderservice] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [orderservice] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [orderservice] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [orderservice] SET ARITHABORT OFF 
GO

ALTER DATABASE [orderservice] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [orderservice] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [orderservice] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [orderservice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [orderservice] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [orderservice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [orderservice] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [orderservice] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [orderservice] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [orderservice] SET  DISABLE_BROKER 
GO

ALTER DATABASE [orderservice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [orderservice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [orderservice] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [orderservice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [orderservice] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [orderservice] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [orderservice] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [orderservice] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [orderservice] SET  MULTI_USER 
GO

ALTER DATABASE [orderservice] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [orderservice] SET DB_CHAINING OFF 
GO

ALTER DATABASE [orderservice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [orderservice] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [orderservice] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [orderservice] SET  READ_WRITE 
GO

USE [orderservice]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](300) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ManagerId] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Rank] [nvarchar](7) NOT NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProducts]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProducts](
	[IdOrder] [int] NOT NULL,
	[IdProduct] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomer] [int] NOT NULL,
	[IdManager] [int] NULL,
	[Date] [date] NOT NULL,
	[Adress] [varchar](150) NOT NULL,
	[Sum] [decimal](18, 0) NOT NULL,
	[CurrentStatus] [nvarchar](9) NOT NULL,
 CONSTRAINT [PK_Orders_1] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
	[ProductRead] [bit] NOT NULL,
	[ProductWrite] [bit] NOT NULL,
	[OrderRead] [bit] NOT NULL,
	[OrderWrite] [bit] NOT NULL,
	[RoleRead] [bit] NOT NULL,
	[RoleWrite] [bit] NOT NULL,
	[UserRead] [bit] NOT NULL,
	[UserWrite] [bit] NOT NULL,
	[ManagerRead] [bit] NOT NULL,
	[ManagerWrite] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_RoleName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[IdRole] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UserName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_ProductRead]  DEFAULT ((0)) FOR [ProductRead]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_ProductWrite]  DEFAULT ((0)) FOR [ProductWrite]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_OrderRead]  DEFAULT ((0)) FOR [OrderRead]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_OrderWrite]  DEFAULT ((0)) FOR [OrderWrite]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_RoleRead]  DEFAULT ((0)) FOR [RoleRead]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_RoleWrite]  DEFAULT ((0)) FOR [RoleWrite]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_UserRead]  DEFAULT ((0)) FOR [UserRead]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_UserWrite]  DEFAULT ((0)) FOR [UserWrite]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_ManagerRead]  DEFAULT ((0)) FOR [ManagerRead]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_ManagerWrite]  DEFAULT ((0)) FOR [ManagerWrite]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Users]
GO
ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_Users]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrderProducts_Orders] FOREIGN KEY([IdOrder])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK_OrderProducts_Orders]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrderProducts_Products] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK_OrderProducts_Products]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[AddCustomer]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCustomer]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	INSERT INTO Customers(IdUser, Name)
	VALUES(@Id, @Name)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[AddFeedback]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddFeedback]
	@Name NVARCHAR(50),
	@Text NVARCHAR(300)
AS
BEGIN
	INSERT INTO Feedbacks(Date, Name, Text)
	VALUES(GETDATE(), @Name, @Text)
END
GO
/****** Object:  StoredProcedure [dbo].[AddManager]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddManager]
	@Id INT,
	@Name NVARCHAR(50),
	@Rank NVARCHAR(7)
AS
BEGIN
	INSERT INTO Managers(IdUser, Name, Rank)
	VALUES(@Id, @Name, @Rank)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[AddOrder]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddOrder]
	@Id INT,
	@Date NVARCHAR(11),
	@Adress NVARCHAR(150),
	@Status NVARCHAR(9),
	@Sum DECIMAL
AS
BEGIN
	INSERT INTO Orders(IdCustomer, Date, Adress, CurrentStatus, Sum)
	VALUES(@Id, @Date, @Adress, @Status, @Sum)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[AddOrderProduct]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddOrderProduct]
	@IdOrder INT,
	@IdProduct INT
AS
BEGIN
	INSERT INTO OrderProducts(IdOrder, IdProduct)
	VALUES(@IdOrder, @IdProduct)
END
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddProduct]
	@Name NVARCHAR(50),
	@Price DECIMAL
AS
BEGIN
	INSERT INTO Products(Name, Price)
	VALUES(@Name, @Price)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[AddRole]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddRole]
	@Name NVARCHAR(15),
	@ProductRead BIT,
	@ProductWrite BIT,
	@OrderRead BIT,
	@OrderWrite BIT,
	@RoleRead BIT,
	@RoleWrite BIT,
	@UserRead BIT,
	@UserWrite BIT,
	@ManagerRead BIT,
	@ManagerWrite BIT
AS
BEGIN
	INSERT INTO Roles(Name, ProductRead, ProductWrite, OrderRead, OrderWrite, RoleRead, RoleWrite, UserRead, UserWrite, ManagerRead, ManagerWrite)
	VALUES(@Name, @ProductRead, @ProductWrite, @OrderRead, @OrderWrite, @RoleRead, @RoleWrite, @UserRead, @UserWrite, @ManagerRead, @ManagerWrite)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddUser]
	@IdRole INT,
	@Name NVARCHAR(15),
	@PasswordHash NVARCHAR(300)
AS
BEGIN
	INSERT INTO Users(IdRole, Name, PasswordHash)
	VALUES(@IdRole, @Name, @PasswordHash)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO
/****** Object:  StoredProcedure [dbo].[CancelOrderById]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CancelOrderById]
	@Id INT
AS
BEGIN
	UPDATE Orders Set CurrentStatus = 'Canceled'
	WHERE OrderId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[CompleteOrderById]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CompleteOrderById]
	@Id INT
AS
BEGIN
	UPDATE Orders Set CurrentStatus = 'Completed'
	WHERE OrderId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllFeedbacks]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllFeedbacks]
AS
BEGIN
	SELECT * FROM Feedbacks
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllManagers]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllManagers]
AS
BEGIN
	SELECT * FROM Managers
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
	SELECT * FROM Products
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllRoles]
AS
BEGIN
	SELECT * FROM Roles
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
	SELECT * FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerByIdUser]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCustomerByIdUser]
	@Id INT
AS
BEGIN
	SELECT CustomerId, Name FROM Customers
	WHERE IdUser = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetManagerByIdUser]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetManagerByIdUser]
	@Id INT
AS
BEGIN
	SELECT ManagerId, Name, Rank FROM Managers
	WHERE IdUser = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetManagerCountByIdUser]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetManagerCountByIdUser]
	@Id INT
AS
BEGIN
	SELECT COUNT(*) FROM Managers
	WHERE IdUser = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewOrders]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNewOrders]
AS
BEGIN
	SELECT OrderId, IdCustomer, Date, Adress, Sum FROM Orders
	WHERE IdManager IS NULL and CurrentStatus = 'Opened'
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByIdCustomer]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrdersByIdCustomer]
	@Id INT
AS
BEGIN
	SELECT OrderId, Date, Adress, Sum, CurrentStatus FROM Orders
	WHERE IdCustomer = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByIdManager]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrdersByIdManager]
	@Id INT
AS
BEGIN
	SELECT OrderId, IdCustomer, Date, Adress, Sum, CurrentStatus FROM Orders
	WHERE IdManager = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductById]
	@Id INT
AS
BEGIN
	SELECT Name, Price FROM Products
	WHERE ProductId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductIdsByIdOrder]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductIdsByIdOrder]
	@Id INT
AS
BEGIN
	SELECT IdProduct FROM OrderProducts
	WHERE IdOrder = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductsCount]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductsCount]
AS
BEGIN
	SELECT COUNT(*) FROM Products
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleById]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoleById]
	@Id INT
AS
BEGIN
	SELECT * FROM Roles
	WHERE RoleId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleIdByName]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoleIdByName]
	@Name NVARCHAR(15),
	@Id INT OUTPUT
AS
BEGIN
	SELECT RoleId FROM Roles
	WHERE Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[GetRolesCount]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRolesCount]
AS
BEGIN
	SELECT COUNT(*) FROM Roles
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByName]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByName]
	@Name NVARCHAR(50)
AS
BEGIN
	SELECT UserId, IdRole, PasswordHash FROM Users
	WHERE Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[InWorkOrder]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InWorkOrder]
	@Id INT,
	@IdManager INT
AS
BEGIN
	UPDATE Orders Set CurrentStatus = 'Processed', IdManager = @IdManager
	WHERE OrderId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveProduct]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveProduct]
	@Id INT
AS
BEGIN
	DELETE FROM Products
	WHERE ProductId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveRole]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveRole]
	@Id INT
AS
BEGIN
	DELETE FROM Roles
	WHERE RoleId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUser]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveUser]
	@Id INT
AS
BEGIN
	DELETE FROM Users
	WHERE UserId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[RestoreOrderById]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestoreOrderById]
	@Id INT
AS
BEGIN
	UPDATE Orders Set CurrentStatus = 'Opened'
	WHERE OrderId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRoleName]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateRoleName]
	@Id INT,
	@Name NVARCHAR(15)
AS
BEGIN
	UPDATE Roles SET Name = @Name
	WHERE RoleId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserName]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateUserName]
	@Id INT,
	@Name NVARCHAR(15)
AS
BEGIN
	UPDATE Users SET Name = @Name
	WHERE UserId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserPasswordHash]    Script Date: 08.10.2019 3:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateUserPasswordHash]
	@Id INT,
	@PasswordHash NVARCHAR(300)
AS
BEGIN
	UPDATE Users SET PasswordHash = @PasswordHash
	WHERE UserId = @Id
END
GO
