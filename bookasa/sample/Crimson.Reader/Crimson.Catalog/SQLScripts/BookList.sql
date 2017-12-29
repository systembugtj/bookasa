USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookList]    Script Date: 04/23/2009 15:40:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookList]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookList]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookList]    Script Date: 04/23/2009 15:40:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookList](
	[ListId] [uniqueidentifier] NOT NULL,
	[ListName] [nvarchar](100) NOT NULL,
	[LastModified] [datetime] NOT NULL,
 CONSTRAINT [PK_BookList] PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

