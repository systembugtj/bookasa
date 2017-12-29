USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookCreator_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookCreator]'))
ALTER TABLE [CrimsonReader].[BookCreator] DROP CONSTRAINT [FK_BookCreator_Book]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookCreator_Creator]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookCreator]'))
ALTER TABLE [CrimsonReader].[BookCreator] DROP CONSTRAINT [FK_BookCreator_Creator]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookCreator]    Script Date: 04/23/2009 15:40:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookCreator]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookCreator]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookCreator]    Script Date: 04/23/2009 15:40:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookCreator](
	[BookId] [uniqueidentifier] NOT NULL,
	[CreatorId] [uniqueidentifier] NOT NULL,
	[Contributor] [bit] NOT NULL,
 CONSTRAINT [PK_BookCreator] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[CreatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[BookCreator]  WITH CHECK ADD  CONSTRAINT [FK_BookCreator_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[BookCreator] CHECK CONSTRAINT [FK_BookCreator_Book]
GO

ALTER TABLE [CrimsonReader].[BookCreator]  WITH CHECK ADD  CONSTRAINT [FK_BookCreator_Creator] FOREIGN KEY([CreatorId])
REFERENCES [CrimsonReader].[Creator] ([CreatorId])
GO

ALTER TABLE [CrimsonReader].[BookCreator] CHECK CONSTRAINT [FK_BookCreator_Creator]
GO

