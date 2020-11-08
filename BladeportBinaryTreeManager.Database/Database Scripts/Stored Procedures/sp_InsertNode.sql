/****** Object:  StoredProcedure [dbo].[sp_InsertNode]    Script Date: 10/21/2020 1:29:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 19, 2020
-- Description:	Stored Procedure to insert a node to a parent node WITH existing children
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertNode]
	@UserId int,
	@ParentId int,
	@SponsorId int,
	@TableName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRANSACTION

	--DECLARE @myRight int; 
	DECLARE @myRightStr nvarchar(100);
	DECLARE @subquery nvarchar(100);
	DECLARE @query nvarchar(max);
	DECLARE @Results TABLE (ResultText NVARCHAR(100));

	SET @subquery ='SELECT rgt FROM ' + @TableName + ' WITH (NOLOCK) WHERE USERID = ' + CONVERT(NVARCHAR(100), @ParentId) 

    INSERT INTO @Results
    EXECUTE SP_EXECUTESQL @subquery

	set @myRightStr  = (SELECT * FROM @Results)

	set @query = 'UPDATE ' + @TableName + ' SET rgt = rgt + 2 WHERE rgt > ' + @myRightStr + ' '
	set @query = @query + 'UPDATE ' + @TableName + ' SET lft = lft + 2 WHERE lft > ' + @myRightStr + ' '
	set @query = @query + 'INSERT INTO ' + @TableName + ' (USERID, lft, rgt, PARENTID, SPONSORID) VALUES (' + 
	                                                       CONVERT(NVARCHAR(100), @UserId) + ', ' + 
														   @myRightStr + ' + 1, ' + 
														   @myRightStr + ' + 2, ' + 
														   CONVERT(NVARCHAR(100), @ParentId) + ', ' +
														   CONVERT(NVARCHAR(100), @SponsorId) + ') '

	EXECUTE sp_executesql @query

	COMMIT TRANSACTION

END
