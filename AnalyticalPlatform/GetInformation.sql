USE [AnalyticalPlatform1]
GO

/****** Object:  StoredProcedure [dbo].[GetInformation]    Script Date: 2014-5-16 22:44:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetInformation] 
AS
BEGIN
select Task,Field,Count from ( select task as Task,'abstract' as Field,count(*) as Count from abstract group by task
union all select task,'author' as field,count(*) as counts from author group by task
union all select task,'authorInstitute' as field,count(*) as counts from authorInstitute group by task
union all select task,'Category' as field,count(*) as counts from Category group by task
union all select task,'Citation' as field,count(*) as counts from Citation group by task
union all select task,'DocumentType' as field,count(*) as counts from DocumentType group by task
union all select task,'Institute' as field,count(*) as counts from Institute group by task
union all select task,'Keyword' as field,count(*) as counts from Keyword group by task
union all select task,'Paper' as field,count(*) as counts from Paper group by task
union all select task,'Reference' as field,count(*) as counts from Reference group by task
union all select task,'ReprintAuthor' as field,count(*) as counts from ReprintAuthor group by task
union all select task,'Fund' as field,count(*) as counts from Fund group by task)t
order by task desc,field
END



GO


