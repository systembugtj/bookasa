USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_EditionBinding_Binding]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[EditionBinding]'))
ALTER TABLE [CrimsonReader].[EditionBinding] DROP CONSTRAINT [FK_EditionBinding_Binding]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_EditionBinding_Edition]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[EditionBinding]'))
ALTER TABLE [CrimsonReader].[EditionBinding] DROP CONSTRAINT [FK_EditionBinding_Edition]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[EditionBinding]    Script Date: 04/23/2009 16:03:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[EditionBinding]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[EditionBinding]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[EditionBinding]    Script Date: 04/23/2009 16:03:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[EditionBinding](
	[EditionId] [uniqueidentifier] NOT NULL,
	[BindingId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookBinding] PRIMARY KEY CLUSTERED 
(
	[EditionId] ASC,
	[BindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[EditionBinding]  WITH CHECK ADD  CONSTRAINT [FK_EditionBinding_Binding] FOREIGN KEY([BindingId])
REFERENCES [CrimsonReader].[Binding] ([BindingId])
GO

ALTER TABLE [CrimsonReader].[EditionBinding] CHECK CONSTRAINT [FK_EditionBinding_Binding]
GO

ALTER TABLE [CrimsonReader].[EditionBinding]  WITH CHECK ADD  CONSTRAINT [FK_EditionBinding_Edition] FOREIGN KEY([EditionId])
REFERENCES [CrimsonReader].[Edition] ([EditionId])
GO

ALTER TABLE [CrimsonReader].[EditionBinding] CHECK CONSTRAINT [FK_EditionBinding_Edition]
GO

