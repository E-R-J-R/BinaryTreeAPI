SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 22, 2020
-- Description:	Get Leaf Nodes of a Tree
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetLeafNodes]
	@TableName nvarchar(100)
AS
BEGIN
	
	DECLARE @query nvarchar(max);

    set @query = 'SELECT b.USERID AS NODEID, b.lft, b.rgt, b.PARENTID
				  FROM ' + @TableName +' b
				  JOIN Users u ON u.USERID = b.USERID
				  WHERE b.rgt = b.lft + 1;'

	EXECUTE sp_executesql @query

END
GO
