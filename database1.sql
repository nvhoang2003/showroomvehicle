USE [master]
GO
/****** Object:  Database [showroom]    Script Date: 12/20/2023 10:56:49 PM ******/
CREATE DATABASE [showroom]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'showroom', FILENAME = N'D:\SSMS\MSSQL16.MSSQLSERVER\MSSQL\DATA\showroom.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'showroom_log', FILENAME = N'D:\SSMS\MSSQL16.MSSQLSERVER\MSSQL\DATA\showroom_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [showroom] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [showroom].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [showroom] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [showroom] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [showroom] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [showroom] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [showroom] SET ARITHABORT OFF 
GO
ALTER DATABASE [showroom] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [showroom] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [showroom] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [showroom] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [showroom] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [showroom] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [showroom] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [showroom] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [showroom] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [showroom] SET  DISABLE_BROKER 
GO
ALTER DATABASE [showroom] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [showroom] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [showroom] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [showroom] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [showroom] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [showroom] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [showroom] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [showroom] SET RECOVERY FULL 
GO
ALTER DATABASE [showroom] SET  MULTI_USER 
GO
ALTER DATABASE [showroom] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [showroom] SET DB_CHAINING OFF 
GO
ALTER DATABASE [showroom] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [showroom] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [showroom] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [showroom] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'showroom', N'ON'
GO
ALTER DATABASE [showroom] SET QUERY_STORE = ON
GO
ALTER DATABASE [showroom] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [showroom]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nchar](50) NULL,
	[password] [nchar](255) NULL,
	[first_name] [nchar](50) NULL,
	[last_name] [nchar](50) NULL,
	[date_of_birth] [date] NULL,
	[phone_number] [nchar](10) NULL,
	[email] [nchar](50) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_order]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_order](
	[customer_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__customer__49005DA7A83A926B] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_service]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_service](
	[service_id] [int] NOT NULL,
	[customer_id] [int] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_customer_service] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC,
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[group_objects]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_objects](
	[group_id] [int] NOT NULL,
	[object_id] [int] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[group_id] ASC,
	[object_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[name] [nchar](50) NULL,
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nchar](50) NULL,
 CONSTRAINT [PK_group] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[objects]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[objects](
	[object_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NULL,
	[description] [nchar](50) NULL,
 CONSTRAINT [PK_object] PRIMARY KEY CLUSTERED 
(
	[object_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[price] [float] NULL,
	[time_create] [date] NULL,
	[manage_by] [int] NULL,
	[status] [nchar](10) NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[organizations]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[organizations](
	[organization_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NULL,
	[description] [nchar](50) NULL,
 CONSTRAINT [PK_organizations] PRIMARY KEY CLUSTERED 
(
	[organization_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pre_order]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pre_order](
	[vehicle_id] [int] NOT NULL,
	[customer_id] [int] NOT NULL,
	[status] [nchar](10) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC,
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase_order]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase_order](
	[purchase_id] [int] IDENTITY(1,1) NOT NULL,
	[date_purchase] [date] NULL,
	[manage_by] [int] NULL,
 CONSTRAINT [PK_purchase_order] PRIMARY KEY CLUSTERED 
(
	[purchase_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[service_order]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_order](
	[service_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NULL,
	[time_create] [date] NULL,
	[description] [text] NULL,
	[manage_by] [int] NULL,
	[customer_id] [int] NULL,
 CONSTRAINT [PK_Table_2] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nchar](50) NULL,
	[password] [nchar](255) NULL,
	[first_name] [nchar](50) NULL,
	[last_name] [nchar](50) NULL,
	[phone_number] [nchar](10) NULL,
	[address] [nchar](50) NULL,
	[manage_id] [int] NULL,
	[group_id] [int] NULL,
	[organization_id] [int] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle](
	[model_number] [nchar](10) NOT NULL,
	[name] [nchar](255) NULL,
	[branch] [nchar](50) NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_vehicle] PRIMARY KEY CLUSTERED 
(
	[model_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle_data]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle_data](
	[vehicle_data_id] [int] NOT NULL,
	[model_number] [nchar](10) NULL,
	[color] [nchar](50) NULL,
	[listId] [text] NULL,
 CONSTRAINT [PK_vehical_data] PRIMARY KEY CLUSTERED 
(
	[vehicle_data_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle_image]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle_image](
	[image_id] [int] IDENTITY(1,1) NOT NULL,
	[vehicle_id] [int] NULL,
	[image_url] [nvarchar](255) NULL,
 CONSTRAINT [PK_vehicle_image] PRIMARY KEY CLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle_order]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle_order](
	[vehicle_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[id_number] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC,
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle_purchase]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle_purchase](
	[vehicle_id] [int] NOT NULL,
	[purchase_order_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[price] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC,
	[purchase_order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vehicle_service]    Script Date: 12/20/2023 10:56:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehicle_service](
	[vehicle_id] [int] NOT NULL,
	[service_id] [int] NOT NULL,
	[id_number] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC,
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK__customer___order__571DF1D5] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK__customer___order__571DF1D5]
GO
ALTER TABLE [dbo].[customer_order]  WITH CHECK ADD  CONSTRAINT [FK_customer_order_customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([customer_id])
GO
ALTER TABLE [dbo].[customer_order] CHECK CONSTRAINT [FK_customer_order_customer]
GO
ALTER TABLE [dbo].[customer_service]  WITH CHECK ADD  CONSTRAINT [FK_customer_service_customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([customer_id])
GO
ALTER TABLE [dbo].[customer_service] CHECK CONSTRAINT [FK_customer_service_customer]
GO
ALTER TABLE [dbo].[customer_service]  WITH CHECK ADD  CONSTRAINT [FK_customer_service_service_order] FOREIGN KEY([service_id])
REFERENCES [dbo].[service_order] ([service_id])
GO
ALTER TABLE [dbo].[customer_service] CHECK CONSTRAINT [FK_customer_service_service_order]
GO
ALTER TABLE [dbo].[group_objects]  WITH CHECK ADD  CONSTRAINT [FK__group_obj__group__5812160E] FOREIGN KEY([group_id])
REFERENCES [dbo].[groups] ([group_id])
GO
ALTER TABLE [dbo].[group_objects] CHECK CONSTRAINT [FK__group_obj__group__5812160E]
GO
ALTER TABLE [dbo].[group_objects]  WITH CHECK ADD  CONSTRAINT [FK__group_obj__objec__59063A47] FOREIGN KEY([object_id])
REFERENCES [dbo].[objects] ([object_id])
GO
ALTER TABLE [dbo].[group_objects] CHECK CONSTRAINT [FK__group_obj__objec__59063A47]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_order_user] FOREIGN KEY([manage_by])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_order_user]
GO
ALTER TABLE [dbo].[pre_order]  WITH CHECK ADD  CONSTRAINT [FK__pre_order__custo__5AEE82B9] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([customer_id])
GO
ALTER TABLE [dbo].[pre_order] CHECK CONSTRAINT [FK__pre_order__custo__5AEE82B9]
GO
ALTER TABLE [dbo].[pre_order]  WITH CHECK ADD  CONSTRAINT [FK__pre_order__vehic__5BE2A6F2] FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle_data] ([vehicle_data_id])
GO
ALTER TABLE [dbo].[pre_order] CHECK CONSTRAINT [FK__pre_order__vehic__5BE2A6F2]
GO
ALTER TABLE [dbo].[purchase_order]  WITH CHECK ADD  CONSTRAINT [FK_purchase_order_user] FOREIGN KEY([manage_by])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[purchase_order] CHECK CONSTRAINT [FK_purchase_order_user]
GO
ALTER TABLE [dbo].[service_order]  WITH CHECK ADD  CONSTRAINT [FK_service_order_customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([customer_id])
GO
ALTER TABLE [dbo].[service_order] CHECK CONSTRAINT [FK_service_order_customer]
GO
ALTER TABLE [dbo].[service_order]  WITH CHECK ADD  CONSTRAINT [FK_service_order_user] FOREIGN KEY([manage_by])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[service_order] CHECK CONSTRAINT [FK_service_order_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[groups] ([group_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_groups]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_organizations] FOREIGN KEY([organization_id])
REFERENCES [dbo].[organizations] ([organization_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_organizations]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_user] FOREIGN KEY([manage_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_user]
GO
ALTER TABLE [dbo].[vehicle_data]  WITH CHECK ADD  CONSTRAINT [FK_vehical_data_vehical_data] FOREIGN KEY([model_number])
REFERENCES [dbo].[vehicle] ([model_number])
GO
ALTER TABLE [dbo].[vehicle_data] CHECK CONSTRAINT [FK_vehical_data_vehical_data]
GO
ALTER TABLE [dbo].[vehicle_image]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_image_vehicle_data] FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle_data] ([vehicle_data_id])
GO
ALTER TABLE [dbo].[vehicle_image] CHECK CONSTRAINT [FK_vehicle_image_vehicle_data]
GO
ALTER TABLE [dbo].[vehicle_order]  WITH CHECK ADD  CONSTRAINT [FK__vehicle_o__order__6477ECF3] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[vehicle_order] CHECK CONSTRAINT [FK__vehicle_o__order__6477ECF3]
GO
ALTER TABLE [dbo].[vehicle_order]  WITH CHECK ADD  CONSTRAINT [FK__vehicle_o__vehic__656C112C] FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle_data] ([vehicle_data_id])
GO
ALTER TABLE [dbo].[vehicle_order] CHECK CONSTRAINT [FK__vehicle_o__vehic__656C112C]
GO
ALTER TABLE [dbo].[vehicle_purchase]  WITH CHECK ADD  CONSTRAINT [FK__vehicle_p__purch__66603565] FOREIGN KEY([purchase_order_id])
REFERENCES [dbo].[purchase_order] ([purchase_id])
GO
ALTER TABLE [dbo].[vehicle_purchase] CHECK CONSTRAINT [FK__vehicle_p__purch__66603565]
GO
ALTER TABLE [dbo].[vehicle_purchase]  WITH CHECK ADD FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle_data] ([vehicle_data_id])
GO
ALTER TABLE [dbo].[vehicle_service]  WITH CHECK ADD FOREIGN KEY([vehicle_id])
REFERENCES [dbo].[vehicle_data] ([vehicle_data_id])
GO
ALTER TABLE [dbo].[vehicle_service]  WITH CHECK ADD  CONSTRAINT [FK_vehicle_service_service_order] FOREIGN KEY([service_id])
REFERENCES [dbo].[service_order] ([service_id])
GO
ALTER TABLE [dbo].[vehicle_service] CHECK CONSTRAINT [FK_vehicle_service_service_order]
GO
USE [master]
GO
ALTER DATABASE [showroom] SET  READ_WRITE 
GO
