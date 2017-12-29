USE [Applications_42237]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[CrimsonReader].[FK_BookListMember_Book]') AND parent_object_id = OBJECT_ID(N'[CrimsonReader].[BookListMember]'))
ALTER TABLE [CrimsonReader].[BookListMember] DROP CONSTRAINT [FK_BookListMember_Book]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookListMember]    Script Date: 04/23/2009 15:41:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CrimsonReader].[BookListMember]') AND type in (N'U'))
DROP TABLE [CrimsonReader].[BookListMember]
GO

USE [Applications_42237]
GO

/****** Object:  Table [CrimsonReader].[BookListMember]    Script Date: 04/23/2009 15:41:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CrimsonReader].[BookListMember](
	[ListId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookListMember] PRIMARY KEY CLUSTERED 
(
	[ListId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CrimsonReader].[BookListMember]  WITH CHECK ADD  CONSTRAINT [FK_BookListMember_Book] FOREIGN KEY([BookId])
REFERENCES [CrimsonReader].[Book] ([BookId])
GO

ALTER TABLE [CrimsonReader].[BookListMember] CHECK CONSTRAINT [FK_BookListMember_Book]
GO

