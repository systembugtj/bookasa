USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Catalog]    Script Date: 04/23/2009 15:46:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Catalog]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Catalog]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Catalog]    Script Date: 04/23/2009 15:47:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Catalog](
	[CatalogId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Uri] [nvarchar](512) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CatalogUri] [nvarchar](512) NOT NULL,
	[CatalogType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[CatalogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

