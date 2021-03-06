USE [master]
GO
/****** Object:  Database [MvcEShop_DB]    Script Date: 5/12/2020 11:26:42 PM ******/
CREATE DATABASE [MvcEShop_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyEshop_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MyEshop_DB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyEshop_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MyEshop_DB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MvcEShop_DB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MvcEShop_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MvcEShop_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MvcEShop_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MvcEShop_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MvcEShop_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MvcEShop_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [MvcEShop_DB] SET  MULTI_USER 
GO
ALTER DATABASE [MvcEShop_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MvcEShop_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MvcEShop_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MvcEShop_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MvcEShop_DB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MvcEShop_DB]
GO
/****** Object:  Table [dbo].[Product_Galleries]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Galleries](
	[GalleryID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[GalleryImageName] [varchar](50) NOT NULL,
	[RecordDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
	[GalleryTitle] [varchar](50) NULL,
 CONSTRAINT [PK_Product_Galleries] PRIMARY KEY CLUSTERED 
(
	[GalleryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product_Groups]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupTitle] [nvarchar](400) NOT NULL,
	[ParentID] [int] NULL,
	[RecordDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Product_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product_Selected_Groups]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Selected_Groups](
	[ProductGroupID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[RecordDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Prodct_Selected_Groups] PRIMARY KEY CLUSTERED 
(
	[ProductGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product_Tags]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Tags](
	[ProductTagID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductTag] [nvarchar](250) NOT NULL,
	[RecordDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Product_Tags] PRIMARY KEY CLUSTERED 
(
	[ProductTagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTitle] [nvarchar](350) NOT NULL,
	[ProductShortDesc] [nvarchar](500) NOT NULL,
	[ProductText] [nvarchar](max) NOT NULL,
	[ProductPrice] [int] NOT NULL,
	[ProductImageName] [varchar](50) NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] NOT NULL,
	[RoleTitle] [nvarchar](250) NOT NULL,
	[RoleName] [varchar](150) NOT NULL,
	[RegisterDate] [datetime] NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/12/2020 11:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[UserEmail] [nvarchar](250) NOT NULL,
	[UserPassword] [varchar](200) NOT NULL,
	[UserActiveCode] [varchar](50) NOT NULL,
	[UserIsActive] [bit] NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Product_Galleries]  WITH CHECK ADD  CONSTRAINT [FK_Product_Galleries_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Product_Galleries] CHECK CONSTRAINT [FK_Product_Galleries_Products]
GO
ALTER TABLE [dbo].[Product_Groups]  WITH CHECK ADD  CONSTRAINT [FK_Product_Groups_Product_Groups] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Product_Groups] ([GroupID])
GO
ALTER TABLE [dbo].[Product_Groups] CHECK CONSTRAINT [FK_Product_Groups_Product_Groups]
GO
ALTER TABLE [dbo].[Product_Selected_Groups]  WITH CHECK ADD  CONSTRAINT [FK_Prodct_Selected_Groups_Product_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Product_Groups] ([GroupID])
GO
ALTER TABLE [dbo].[Product_Selected_Groups] CHECK CONSTRAINT [FK_Prodct_Selected_Groups_Product_Groups]
GO
ALTER TABLE [dbo].[Product_Selected_Groups]  WITH CHECK ADD  CONSTRAINT [FK_Prodct_Selected_Groups_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Product_Selected_Groups] CHECK CONSTRAINT [FK_Prodct_Selected_Groups_Products]
GO
ALTER TABLE [dbo].[Product_Tags]  WITH CHECK ADD  CONSTRAINT [FK_Product_Tags_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Product_Tags] CHECK CONSTRAINT [FK_Product_Tags_Products]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [MvcEShop_DB] SET  READ_WRITE 
GO
