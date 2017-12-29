USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Creator]    Script Date: 04/23/2009 15:47:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Creator]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Creator]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Creator]    Script Date: 04/23/2009 15:47:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Creator](
	[CreatorId] [uniqueidentifier] NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[GivenName] [nvarchar](100) NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[Born] [int] NULL,
	[Died] [int] NULL,
	[WikiPediaUri] [nvarchar](512) NULL,
 CONSTRAINT [PK_Creator] PRIMARY KEY CLUSTERED 
(
	[CreatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

