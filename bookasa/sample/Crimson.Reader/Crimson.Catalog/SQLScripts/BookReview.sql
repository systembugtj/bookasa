USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookReview_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookReview]'))
ALTER TABLE [CrimsonReader].[BookReview] DROP CONSTRAINT [FK_BookReview_Book]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookReview]    Script Date: 04/23/2009 15:45:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookReview]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookReview]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookReview]    Script Date: 04/23/2009 15:45:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookReview](
	[BookId] [uniqueidentifier] NOT NULL,
	[Review] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookReview] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[Review] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[BookReview]  WITH CHECK ADD  CONSTRAINT [FK_BookReview_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[BookReview] CHECK CONSTRAINT [FK_BookReview_Book]
GO

