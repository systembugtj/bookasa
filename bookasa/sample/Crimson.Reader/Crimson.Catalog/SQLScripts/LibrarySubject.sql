USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[LibrarySubject]    Script Date: 04/23/2009 16:04:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[LibrarySubject]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[LibrarySubject]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[LibrarySubject]    Script Date: 04/23/2009 16:04:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[LibrarySubject](
	[SubjectId] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_LibrarySubject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

