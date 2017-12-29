USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookSubject_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookSubject]'))
ALTER TABLE [CrimsonReader].[BookSubject] DROP CONSTRAINT [FK_BookSubject_Book]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookSubject_LibrarySubject]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookSubject]'))
ALTER TABLE [CrimsonReader].[BookSubject] DROP CONSTRAINT [FK_BookSubject_LibrarySubject]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookSubject]    Script Date: 04/23/2009 15:46:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookSubject]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookSubject]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookSubject]    Script Date: 04/23/2009 15:46:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookSubject](
	[LCSHId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookSubject] PRIMARY KEY CLUSTERED 
(
	[LCSHId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[BookSubject]  WITH CHECK ADD  CONSTRAINT [FK_BookSubject_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[BookSubject] CHECK CONSTRAINT [FK_BookSubject_Book]
GO

ALTER TABLE [CrimsonReader].[BookSubject]  WITH CHECK ADD  CONSTRAINT [FK_BookSubject_LibrarySubject] FOREIGN KEY([LCSHId])
REFERENCES [CrimsonReader].[LibrarySubject] ([SubjectId])
GO

ALTER TABLE [CrimsonReader].[BookSubject] CHECK CONSTRAINT [FK_BookSubject_LibrarySubject]
GO

