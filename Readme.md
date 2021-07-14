  KANBAN_MASTER
  
Creating a Warehouse kanban system using ASPNET MVC This is a wharehouse KANBAN printing system that utilises MSSQL and ASPNET MVC. System that is current running on is MSSQL Server 2021 MVC that the system being developed on is ASPNET MVC 5.

This system has 2 table which inclused the KANBAN MASTER and the SUPERMARKET ADDRESS

which is created via :

  CREATE SUPERMARKET SLIDER
  
USE [IBusinessTest] GO

/****** Object: Table [dbo].[SUPERMARKET_SLIDER] Script Date: 14/07/2021 12:03:47 PM ******/ SET ANSI_NULLS ON GO

SET QUOTED_IDENTIFIER ON GO

CREATE TABLE [dbo].[SUPERMARKET_SLIDER]( [ID] [int] IDENTITY(1,1) NOT NULL, [SLIDER_ADDRESS] nvarchar NULL, [PART_NUMBER] nvarchar NULL, [STATUS] nvarchar NULL, [RACK] nvarchar NULL, [AREA] nvarchar NULL, [BIN] nvarchar NULL, [COLOR] nvarchar NULL, [MULTIPLE_PLART] [bit] NULL ) ON [PRIMARY] GO

  CREATE KANBAN MASTER
  
USE [IBusinessTest] GO

/****** Object: Table [dbo].[KANBAN_MASTER] Script Date: 14/07/2021 12:04:24 PM ******/ SET ANSI_NULLS ON GO

SET QUOTED_IDENTIFIER ON GO

CREATE TABLE [dbo].[KANBAN_MASTER]( [ID] [int] IDENTITY(1,1) NOT NULL, [PHOTO] [image] NULL, [PART_NO] nvarchar NULL, [PART_NAME] [text] NULL, [MODEL] nvarchar NULL, [LINE] [text] NULL, [OUTPUT] [int] NULL, [USAGE] [int] NULL, [PROCESS] nvarchar NULL, [QTY_PER_TRAY] [int] NULL, [TARY_PER_BIN] [int] NULL, [QTY_PER_BIN] [int] NULL, [BIN_TYPE] nvarchar NULL, [LOCATION] nvarchar NULL, [SLIDER_ADDRESS] [text] NULL, [KITTING_SLIDER] nvarchar NULL, [STORE_KANBAN_QTY] [int] NULL, [PROD_KANBAN_QTY] [int] NULL, [BASIC_FINISH_DATE] [date] NULL, [REVISION] [text] NULL, [BIN_NUMBER_END] nvarchar NULL, [CYCLE_TIME] [int] NULL, [SUPPLIER] nvarchar NULL, [REMARKS] [text] NULL ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO
