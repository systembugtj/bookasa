USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Review]    Script Date: 04/23/2009 16:04:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Review]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Review]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Review]    Script Date: 04/23/2009 16:04:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Review](
	[ReviewId] [uniqueidentifier] NOT NULL,
	[ReviewUri] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

