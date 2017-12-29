USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Series]    Script Date: 04/23/2009 16:05:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Series]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Series]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Series]    Script Date: 04/23/2009 16:05:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Series](
	[SeriesId] [uniqueidentifier] NOT NULL,
	[SeriesTitle] [nvarchar](90) NOT NULL,
 CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
(
	[SeriesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

