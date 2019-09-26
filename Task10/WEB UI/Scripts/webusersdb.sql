USE [master]
GO

/****** Object:  Database [webusersdb]    Script Date: 26.09.2019 20:34:55 ******/
CREATE DATABASE [webusersdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'webusersdb', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\webusersdb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'webusersdb_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\webusersdb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [webusersdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [webusersdb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [webusersdb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [webusersdb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [webusersdb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [webusersdb] SET ARITHABORT OFF 
GO

ALTER DATABASE [webusersdb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [webusersdb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [webusersdb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [webusersdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [webusersdb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [webusersdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [webusersdb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [webusersdb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [webusersdb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [webusersdb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [webusersdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [webusersdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [webusersdb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [webusersdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [webusersdb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [webusersdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [webusersdb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [webusersdb] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [webusersdb] SET  MULTI_USER 
GO

ALTER DATABASE [webusersdb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [webusersdb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [webusersdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [webusersdb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [webusersdb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [webusersdb] SET  READ_WRITE 
GO

USE [webusersdb]
GO
/****** Object:  Table [dbo].[AltImages]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AltImages](
	[Guid] [uniqueidentifier] NOT NULL,
	[Bytes] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_AltImages] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[Guid] [uniqueidentifier] NOT NULL,
	[ImageGuid] [uniqueidentifier] NULL,
	[Title] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Guid] [uniqueidentifier] NOT NULL,
	[Bytes] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Guid] [uniqueidentifier] NOT NULL,
	[ImageGuid] [uniqueidentifier] NULL,
	[Name] [nvarchar](150) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [tinyint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersAwards]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersAwards](
	[GuidUser] [uniqueidentifier] NOT NULL,
	[GuidAward] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Webroles]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Webroles](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Webroles] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Webroles] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Webusers]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Webusers](
	[IdWebuser] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Webusers] PRIMARY KEY CLUSTERED 
(
	[IdWebuser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Webusers] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Awards]  WITH CHECK ADD  CONSTRAINT [FK_Awards_Images] FOREIGN KEY([ImageGuid])
REFERENCES [dbo].[Images] ([Guid])
GO
ALTER TABLE [dbo].[Awards] CHECK CONSTRAINT [FK_Awards_Images]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Images] FOREIGN KEY([ImageGuid])
REFERENCES [dbo].[Images] ([Guid])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Images]
GO
ALTER TABLE [dbo].[UsersAwards]  WITH CHECK ADD  CONSTRAINT [FK_UsersAwards_Awards] FOREIGN KEY([GuidAward])
REFERENCES [dbo].[Awards] ([Guid])
GO
ALTER TABLE [dbo].[UsersAwards] CHECK CONSTRAINT [FK_UsersAwards_Awards]
GO
ALTER TABLE [dbo].[UsersAwards]  WITH CHECK ADD  CONSTRAINT [FK_UsersAwards_Users] FOREIGN KEY([GuidUser])
REFERENCES [dbo].[Users] ([Guid])
GO
ALTER TABLE [dbo].[UsersAwards] CHECK CONSTRAINT [FK_UsersAwards_Users]
GO
ALTER TABLE [dbo].[Webusers]  WITH CHECK ADD  CONSTRAINT [FK_Webusers_Webroles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Webroles] ([IdRole])
GO
ALTER TABLE [dbo].[Webusers] CHECK CONSTRAINT [FK_Webusers_Webroles]
GO
/****** Object:  StoredProcedure [dbo].[AddAward]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAward]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(150)
AS
BEGIN
	INSERT INTO Awards (Guid, Title)
	VALUES(@Guid, @Title)
END
GO
/****** Object:  StoredProcedure [dbo].[AddImage]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddImage]
	@Guid UNIQUEIDENTIFIER,
	@Bytes VARBINARY(MAX)
AS
BEGIN
	INSERT INTO Images(Guid, Bytes)
	VALUES (@Guid, @Bytes)
END
GO
/****** Object:  StoredProcedure [dbo].[AddImageToAward]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddImageToAward]
	@Guid UNIQUEIDENTIFIER,
	@ImageGuid UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE Awards SET ImageGuid = @ImageGuid
	WHERE Guid = @Guid 
END
GO
/****** Object:  StoredProcedure [dbo].[AddImageToUser]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddImageToUser]
	@Guid UNIQUEIDENTIFIER,
	@ImageGuid UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE Users SET ImageGuid = @ImageGuid
	WHERE Guid = @Guid 
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddUser]
	@Guid UNIQUEIDENTIFIER,
	@Name NVARCHAR(150),
	@DateOfBirth DATE,
	@Age TINYINT
AS
BEGIN
	INSERT INTO Users(Guid, Name, DateOfBirth, Age)
	VALUES(@Guid, @Name, @DateOfBirth, @Age)
END
GO
/****** Object:  StoredProcedure [dbo].[AddWebrole]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddWebrole] 
	@RoleName NVARCHAR(50)
AS
BEGIN
	INSERT INTO WebRoles(Name)
	VALUES(@RoleName)
END
GO
/****** Object:  StoredProcedure [dbo].[AddWebuser]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddWebuser] 
	@RoleId int, 
	@UserName NVARCHAR(50),
	@PasswordHash NVARCHAR(500) 
AS
BEGIN
	INSERT INTO Webusers(RoleId, Name, PasswordHash)
	VALUES(@RoleId, @UserName, @PasswordHash)
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteWebrole]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteWebrole] 
	@RoleId INT
AS
BEGIN
	DELETE FROM WebRoles
	WHERE IdRole = @RoleId 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllAwards]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllAwards]
AS
BEGIN
	SELECT * FROM Awards
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 26.09.2019 20:35:55 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllUsersAwards]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllUsersAwards]
AS
BEGIN
	SELECT * FROM UsersAwards
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllWebusers]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllWebusers]
AS
BEGIN
	SELECT * FROM Webusers
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardByGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAwardByGuid]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(150) OUTPUT
AS
BEGIN
	SELECT Title FROM Awards
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardGuidsByUserGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAwardGuidsByUserGuid]
	@UserGuid UNIQUEIDENTIFIER
AS
BEGIN
	SELECT GuidAward FROM UsersAwards
	WHERE GuidUser = @UserGuid 
END
GO
/****** Object:  StoredProcedure [dbo].[GetIdRoleByName]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetIdRoleByName]
	@RoleName NVARCHAR(50),
	@IdRole int OUTPUT
AS
BEGIN
	SELECT IdRole FROM Webroles
	WHERE Name = @RoleName 
END
GO
/****** Object:  StoredProcedure [dbo].[GetImageByGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetImageByGuid]
	@Guid UNIQUEIDENTIFIER
AS
BEGIN
	SELECT Bytes FROM Images
	WHERE Guid = @Guid 
END
GO
/****** Object:  StoredProcedure [dbo].[GetImageGuidByAwardGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetImageGuidByAwardGuid]
	@AwardGuid UNIQUEIDENTIFIER,
	@ImageGuid UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SELECT ImageGuid FROM Awards
	WHERE Guid = @AwardGuid 
END
GO
/****** Object:  StoredProcedure [dbo].[GetImageGuidByUserGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetImageGuidByUserGuid]
	@UserGuid UNIQUEIDENTIFIER,
	@ImageGuid UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SELECT ImageGuid FROM Users
	WHERE Guid = @UserGuid 
END
GO
/****** Object:  StoredProcedure [dbo].[GetPasswordHashFormUserName]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPasswordHashFormUserName]
	@UserName NVARCHAR(50),
	@PasswordHash NVARCHAR(500) OUTPUT
AS
BEGIN
	SELECT PasswordHash FROM Webusers
	WHERE Name = @UserName 
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleIdByUserName]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoleIdByUserName]
	@UserName NVARCHAR(50),
	@RoleId int OUTPUT
AS
BEGIN
	SELECT RoleId FROM Webusers
	WHERE Name = @UserName 
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleNameByIdRole]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoleNameByIdRole]
	@IdRole int,
	@RoleName NVARCHAR(50) OUTPUT
AS
BEGIN
	SELECT Name FROM Webroles
	WHERE IdRole = @IdRole
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByGuid]
	@Guid UNIQUEIDENTIFIER,
	@Name NVARCHAR(150) OUTPUT,
	@DateOfBirth DATE OUTPUT
AS
BEGIN
	SELECT Name, DateOfBirth FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[JoinAwardToUser]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[JoinAwardToUser]
	@GuidUser UNIQUEIDENTIFIER,
	@GuidAward UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO UsersAwards(GuidUser, GuidAward)
	VALUES(@GuidUser, @GuidAward)
END
GO
/****** Object:  StoredProcedure [dbo].[PrintAwardsInfo]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrintAwardsInfo] 
AS
BEGIN
	SELECT *
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'Awards'
END
GO
/****** Object:  StoredProcedure [dbo].[PrintUsersAwardsInfo]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrintUsersAwardsInfo] 
AS
BEGIN
	SELECT *
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'UsersAwards'
END
GO
/****** Object:  StoredProcedure [dbo].[PrintUsersInfo]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrintUsersInfo] 
AS
BEGIN
	SELECT *
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'Users'
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveAwardByGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveAwardByGuid]
	@Guid UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM Awards
	WHERE Guid = @Guid 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserAwardByAwardGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveUserAwardByAwardGuid]
	@AwardGuid UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM UsersAwards
	WHERE GuidAward = @AwardGuid 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserAwardByUserGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveUserAwardByUserGuid]
	@UserGuid UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM UsersAwards
	WHERE GuidUser = @UserGuid 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserByGuid]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RemoveUserByGuid]
	@Guid UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM Users
	WHERE Guid = @Guid 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleNameCount]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RoleNameCount]
	@RoleName NVARCHAR(50),
	@Count int OUTPUT
AS
BEGIN
	SELECT COUNT(*) FROM Webroles
	WHERE Name = @RoleName 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateWebuserRole]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateWebuserRole]
	@RoleId INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE Webusers SET RoleId = @RoleId
	WHERE Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[UserNameCount]    Script Date: 26.09.2019 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserNameCount]
	@UserName NVARCHAR(50),
	@Count int OUTPUT
AS
BEGIN
	SELECT COUNT(*) FROM Webusers
	WHERE Name = @UserName 
END
GO
