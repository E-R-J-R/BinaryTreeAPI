SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 22, 2020
-- Description:	Retrieve the Full Tree Schema
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetFullTreeSchema]
	@TableName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @query nvarchar(max);

    set @query = 'SELECT node.USERID, NodeUser.USERNAME, CONCAT(NodeUser.FirstName, '' '', NodeUser.LastName) AS FULLNAME, node.PARENTID, node.SPONSORID, d.depth
				  FROM ' +
				  @TableName + ' AS node
				  JOIN Users NodeUser ON NodeUser.USERID = node.USERID
				  JOIN (SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
						FROM ' +
						@TableName + ' AS node, ' +
						@TableName + ' AS parent 
						WHERE node.lft BETWEEN parent.lft AND parent.rgt
						GROUP BY node.USERID ) d ON d.USERID = node.USERID, ' +
				  @TableName + ' AS parent 
				  WHERE node.lft BETWEEN parent.lft AND parent.rgt AND 
					  parent.USERID = (SELECT USERID FROM ' + @TableName + ' WHERE ISMASTERPARENT = 1)
				  ORDER BY node.lft'

	EXECUTE sp_executesql @query

END
GO
