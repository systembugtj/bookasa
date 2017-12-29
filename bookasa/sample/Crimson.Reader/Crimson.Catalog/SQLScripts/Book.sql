USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_Catalogs_Books]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[Book]'))
ALTER TABLE [CrimsonReader].[Book] DROP CONSTRAINT [FK_Catalogs_Books]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Book]    Script Date: 04/23/2009 15:39:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Book]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Book]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Book]    Script Date: 04/23/2009 15:39:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Book](
	[BookId] [uniqueidentifier] NOT NULL,
	[CatalogId] [uniqueidentifier] NOT NULL,
	[BookCatalogId] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](512) NULL,
	[AlternateTitle] [nvarchar](512) NULL,
	[FriendlyTitle] [nvarchar](512) NULL,
	[Created] [date] NOT NULL,
	[Modified] [date] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Catalogs_Books] FOREIGN KEY([CatalogId])
REFERENCES [CrimsonReader].[Catalog] ([CatalogId])
GO

ALTER TABLE [CrimsonReader].[Book] CHECK CONSTRAINT [FK_Catalogs_Books]
GO

