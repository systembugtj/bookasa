USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookSeries_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookSeries]'))
ALTER TABLE [CrimsonReader].[BookSeries] DROP CONSTRAINT [FK_BookSeries_Book]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookSeries_Series]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookSeries]'))
ALTER TABLE [CrimsonReader].[BookSeries] DROP CONSTRAINT [FK_BookSeries_Series]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookSeries]    Script Date: 04/23/2009 15:46:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookSeries]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookSeries]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookSeries]    Script Date: 04/23/2009 15:46:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookSeries](
	[BookId] [uniqueidentifier] NOT NULL,
	[SeriesId] [uniqueidentifier] NOT NULL,
	[VolumeNumber] [int] NULL,
 CONSTRAINT [PK_BookSeries] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[SeriesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[BookSeries]  WITH CHECK ADD  CONSTRAINT [FK_BookSeries_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[BookSeries] CHECK CONSTRAINT [FK_BookSeries_Book]
GO

ALTER TABLE [CrimsonReader].[BookSeries]  WITH CHECK ADD  CONSTRAINT [FK_BookSeries_Series] FOREIGN KEY([SeriesId])
REFERENCES [CrimsonReader].[Series] ([SeriesId])
GO

ALTER TABLE [CrimsonReader].[BookSeries] CHECK CONSTRAINT [FK_BookSeries_Series]
GO

