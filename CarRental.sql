USE [master]
GO
/****** Object:  Database [CarRental]    Script Date: 10.04.2021 15:24:12 ******/
CREATE DATABASE [CarRental]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarRental', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CarRental.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ), 
 FILEGROUP [FILESTREAMGROUP] CONTAINS FILESTREAM  DEFAULT
( NAME = N'CarRental_data', FILENAME = N'E:\CarRentalFileStream\CarRental_data' , MAXSIZE = UNLIMITED)
 LOG ON 
( NAME = N'CarRental_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CarRental_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CarRental] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarRental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarRental] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarRental] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarRental] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarRental] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarRental] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarRental] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarRental] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarRental] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarRental] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarRental] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarRental] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarRental] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarRental] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarRental] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarRental] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarRental] SET RECOVERY FULL 
GO
ALTER DATABASE [CarRental] SET  MULTI_USER 
GO
ALTER DATABASE [CarRental] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarRental] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarRental] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarRental] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarRental] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarRental] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CarRental', N'ON'
GO
ALTER DATABASE [CarRental] SET QUERY_STORE = OFF
GO
USE [CarRental]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Brands] UNIQUE NONCLUSTERED 
(
	[BrandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarImages]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarImages](
	[GUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CarID] [int] NOT NULL,
	[ImageName] [varchar](100) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[ImagePath] [varbinary](max) FILESTREAM  NOT NULL,
 CONSTRAINT [PK_CarImages] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY] FILESTREAM_ON [FILESTREAMGROUP]
) ON [PRIMARY] FILESTREAM_ON [FILESTREAMGROUP]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[ColorID] [int] NOT NULL,
	[ModelYear] [date] NOT NULL,
	[DailyPrice] [money] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Colors] UNIQUE NONCLUSTERED 
(
	[ColorName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditCards]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCards](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[NameOnCard] [varchar](50) NOT NULL,
	[CardNumber] [bigint] NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[CvvNumber] [int] NOT NULL,
 CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_CreditCards_CardNumber] UNIQUE NONCLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CompanyName] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Customers] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileStreamCarRentalFiles]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileStreamCarRentalFiles](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [varchar](500) NULL,
	[FileStream] [varbinary](max) FILESTREAM  NULL,
UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] FILESTREAM_ON [FILESTREAMGROUP]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_OperationClaims] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rentals]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rentals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CarID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[RentDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
 CONSTRAINT [PK_Rentals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperationClaims]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperationClaims](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[OperationClaimID] [int] NOT NULL,
 CONSTRAINT [PK_UserOperationClaims] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.04.2021 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](25) NOT NULL,
	[LastName] [varchar](25) NOT NULL,
	[EMail] [varchar](25) NOT NULL,
	[PasswordHash] [varbinary](500) NOT NULL,
	[PasswordSalt] [varbinary](500) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Users] UNIQUE NONCLUSTERED 
(
	[EMail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Brands]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Colors] FOREIGN KEY([ColorID])
REFERENCES [dbo].[Colors] ([ID])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Colors]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Users]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [CK_CreditCards_CardNumber] CHECK  (([CardNumber]>=(1000000000000000.) AND [CardNumber]<=(9999999999999999.)))
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [CK_CreditCards_CardNumber]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [CK_CreditCards_CvvNumber] CHECK  (([CvvNumber]>=(100) AND [CvvNumber]<=(999)))
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [CK_CreditCards_CvvNumber]
GO
USE [master]
GO
ALTER DATABASE [CarRental] SET  READ_WRITE 
GO
