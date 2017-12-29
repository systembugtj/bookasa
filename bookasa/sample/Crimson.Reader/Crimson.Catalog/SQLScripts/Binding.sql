USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Binding]    Script Date: 04/23/2009 15:39:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[Binding]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[Binding]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[Binding]    Script Date: 04/23/2009 15:39:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[Binding](
	[BindingType] [nvarchar](50) NOT NULL,
	[BindingId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Binding] PRIMARY KEY CLUSTERED 
(
	[BindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

