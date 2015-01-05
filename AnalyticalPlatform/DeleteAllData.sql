USE [AnalyticalPlatform1]
GO

/****** Object:  StoredProcedure [dbo].[DeleteAllData]    Script Date: 2014-5-16 22:43:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAllData]
AS
BEGIN
	truncate table Abstract;
	truncate table Author;
	truncate table AuthorInstitute;
	truncate table Category;
	truncate table Citation;
	truncate table DocumentType;
	truncate table Institute;
	truncate table Keyword;
	truncate table Paper;
	truncate table Reference;
	truncate table ReprintAuthor;
	truncate table Fund;
END


GO


