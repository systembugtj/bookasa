USE [Applications_42237]
GO

/****** Object:  StoredProcedure [dbo].[AddBindingType]    Script Date: 04/22/2009 14:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBindingType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddBindingType]
GO

USE [Applications_42237]
GO

/****** Object:  StoredProcedure [dbo].[AddBindingType]    Script Date: 04/22/2009 14:28:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[AddBindingType]
(
	@BindingType nvarchar(50),
	@BindingId uniqueidentifier
)
AS
INSERT INTO [Applications_42237].[dbo].[Binding]
           ([BindingType]
           ,[BindingId])
     VALUES
           (@BindingType,@BindingId)

GO

