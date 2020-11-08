/****** Object:  StoredProcedure [dbo].[sp_GetSubTreeDepth]    Script Date: 10/22/2020 6:32:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 20, 2020
-- Description:	Gets the nodes in a Subtree with its corresponding depth
-- =============================================
CREATE PROCEDURE  [dbo].[sp_GetSubTreeDepth]
	@ParentId int,
	@TableName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
    DECLARE @query nvarchar(max);

    set @query = 'SELECT node.USERID, 
	             (COUNT(parent.USERID) - (sub_tree.depth + 1)) AS depth,
				 node.LFT,
				 node.RGT,
				 node.PARENTID
				 FROM ' +
				 @TableName + ' AS node, ' +
				 @TableName + ' AS parent, ' +
				 @TableName + ' AS sub_parent, ' +
				 '(
								SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
								FROM ' +  
								@TableName + ' AS node, ' +
								@TableName + ' AS parent 
								WHERE node.lft BETWEEN parent.lft AND parent.rgt
								AND node.USERID = ' + CONVERT(NVARCHAR(100), @ParentId) +
							  ' GROUP BY node.USERID
				 ) AS sub_tree
				 WHERE node.lft BETWEEN parent.lft AND parent.rgt
						AND node.lft BETWEEN sub_parent.lft AND sub_parent.rgt
						AND sub_parent.USERID = sub_tree.USERID
				 GROUP BY node.USERID, sub_tree.depth, node.lft, node.RGT, node.PARENTID
				 ORDER BY node.lft'

	 EXECUTE sp_executesql @query


END
