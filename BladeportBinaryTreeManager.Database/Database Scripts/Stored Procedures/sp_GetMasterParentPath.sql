SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 23, 2020
-- Description:	Gets the node path to the master parent
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetMasterParentPath]
    @UserId int,
	@TableName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @query nvarchar(max);

    set @query =  'SELECT parent.USERID AS NODEID, ParentUser.USERNAME
				   FROM ' +
				   @TableName + ' AS node, ' +
				   @TableName + ' AS parent 
				   JOIN Users ParentUser ON ParentUser.USERID = parent.USERID
				   WHERE node.lft BETWEEN parent.lft AND parent.rgt AND node.USERID = ' + CONVERT(NVARCHAR(100), @UserId) + 
				   ' ORDER BY parent.lft'

	EXECUTE sp_executesql @query
END
GO
