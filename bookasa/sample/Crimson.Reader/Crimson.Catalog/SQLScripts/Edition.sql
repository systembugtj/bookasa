USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_Edition_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[Edition]'))
ALTER TABLE [CrimsonReader].[Edition] DROP CONSTRAINT [FK_Edition_Book]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Edition]    Script Date: 04/23/2009 15:47:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Edition]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Edition]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Edition]    Script Date: 04/23/2009 15:47:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Edition](
	[EditionId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
	[CatalogId] [nvarchar](13) NOT NULL,
	[Modified] [datetime] NOT NULL,
	[InternetUri] [nvarchar](512) NULL,
	[LocalUri] [nvarchar](512) NULL,
	[Publisher] [nvarchar](100) NULL,
	[TableOfContents] [image] NULL,
	[Rights] [nvarchar](512) NULL,
	[Type] [nvarchar](100) NULL,
	[Language] [nvarchar](5) NULL,
	[Date] [nvarchar](20) NULL,
	[Description] [nvarchar](512) NULL,
	[Price] [money] NULL,
	[AgeGroup] [nvarchar](50) NULL,
 CONSTRAINT [PK_Edition] PRIMARY KEY CLUSTERED 
(
	[EditionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[Edition] CHECK CONSTRAINT [FK_Edition_Book]
GO

