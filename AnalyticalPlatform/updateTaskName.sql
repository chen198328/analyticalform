USE [AnalyticalPlatform1]
GO

/****** Object:  StoredProcedure [dbo].[UpdateTaskName]    Script Date: 2014-5-16 22:44:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTaskName]
	@TaskName nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Abstract set Task=@TaskName;
	update Author set Task=@TaskName;
	update AuthorInstitute set Task=@TaskName;
	update Category set Task=@TaskName;
	update Citation set Task=@TaskName;
	update DocumentType set Task=@TaskName;
	update Institute set Task=@TaskName;
	update Keyword set Task=@TaskName;
	update Paper set Task=@TaskName;
	update Reference set Task=@TaskName;
	update ReprintAuthor set Task=@TaskName;
	update Fund set Task=@TaskName;
END


GO


