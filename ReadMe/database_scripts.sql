CREATE TABLE [dbo].[Users](
	[USERID] [int] IDENTITY(1,1) NOT NULL,
	[USERNAME] [nvarchar](50) NOT NULL,
	[FIRSTNAME] [nvarchar](50) NOT NULL,
	[LASTNAME] [nvarchar](50) NOT NULL,
	[JOINDATE] [smalldatetime] NOT NULL
) ON [PRIMARY]

INSERT INTO Users (USERNAME, FIRSTNAME, LASTNAME, JOINDATE) VALUES
 ('kenpark', 'Ken', 'Park', 'October 1, 2020'),
 ('janesmith', 'Jane', 'Smith', 'October 2, 2020'),
 ('ivyviola', 'Ivy', 'Viola', 'October 3, 2020'),
 ('joshbailey', 'Josh', 'Bailey', 'October 3, 2020'),
 ('duncantaylor', 'Duncan', 'Taylor', 'October 4, 2020'),
 ('andersoncooper', 'Anderson', 'Cooper', 'October 5, 2020'),
 ('griffinluna', 'Griffin', 'Luna', 'October 6, 2020'),
 ('finleygray', 'Finley', 'Gray', 'October 6, 2020'),
 ('jennywest', 'Jenny', 'West', 'October 7, 2020'),
 ('shawavery', 'Shaw', 'Avery', 'October 8, 2020'),
 ('jollybee', 'Jolly', 'Bee',  'October 12, 2020'),
 ('mishaken', 'Misha', 'Ken', 'October 14, 2020'),
 ('henryford', 'Henry', 'Ford', 'October 20, 2020'),
 ('kenmiles', 'Ken', 'Miles', 'October 20, 2020'),
 ('mariesy', 'Marie', 'Sy', 'October 20, 2020'),
 ('user16', 'User', '16', 'October 28, 2020'),
 ('user17', 'User', '17', 'October 28, 2020'),
 ('user18', 'User', '18', 'October 28, 2020'),
 ('user19', 'User', '19', 'October 28, 2020'),
 ('user20', 'User', '20', 'October 28, 2020'),
 ('user21', 'User', '21', 'October 28, 2020'),
 ('user22', 'User', '22', 'October 28, 2020'),
 ('user23', 'User', '23', 'October 28, 2020'),
 ('user24', 'User', '24', 'October 28, 2020'),
 ('user25', 'User', '25', 'October 28, 2020'),
 ('user26', 'User', '26', 'October 28, 2020'),
 ('user27', 'User', '27', 'October 28, 2020'),
 ('user28', 'User', '28', 'October 28, 2020'),
 ('user29', 'User', '29', 'October 28, 2020'),
 ('user30', 'User', '30', 'October 28, 2020'),
 ('user31', 'User', '31', 'October 28, 2020'),
 ('user32', 'User', '32', 'October 28, 2020'),
 ('user33', 'User', '33', 'October 28, 2020'),
 ('user34', 'User', '34', 'October 28, 2020'),
 ('user35', 'User', '35', 'October 28, 2020'),
 ('user36', 'User', '36', 'October 28, 2020'),
 ('user37', 'User', '37', 'October 28, 2020'),
 ('user38', 'User', '38', 'October 28, 2020'),
 ('user39', 'User', '39', 'October 28, 2020'),
 ('user40', 'User', '40', 'October 28, 2020'),
 ('user41', 'User', '41', 'October 28, 2020'),
 ('user42', 'User', '42', 'October 28, 2020'),
 ('user43', 'User', '43', 'October 28, 2020'),
 ('user44', 'User', '44', 'October 28, 2020'),
 ('user45', 'User', '45', 'October 28, 2020'),
 ('user46', 'User', '46', 'October 28, 2020'),
 ('user47', 'User', '47', 'October 28, 2020'),
 ('user48', 'User', '48', 'October 28, 2020'),
 ('user49', 'User', '49', 'October 28, 2020'),
 ('user50', 'User', '50', 'October 28, 2020'),
 ('user51', 'User', '51', 'October 28, 2020'),
 ('user52', 'User', '52', 'October 28, 2020'),
 ('user53', 'User', '53', 'October 28, 2020'),
 ('user54', 'User', '54', 'October 28, 2020'),
 ('user55', 'User', '55', 'October 28, 2020'),
 ('user56', 'User', '56', 'October 28, 2020'),
 ('user57', 'User', '57', 'October 28, 2020'),
 ('user58', 'User', '58', 'October 28, 2020')
 
 CREATE TABLE BinaryTree (
        USERID INT NOT NULL UNIQUE,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

CREATE TABLE ForcedMatrix3x3 (
        USERID INT NOT NULL,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

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

GO
-- =============================================
-- Author:		Efren Recio
-- Create date: October 19, 2020
-- Description:	Stored Procedure to insert a node to a parent node WITHOUT children
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertLeafNode]
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

	DECLARE @myLeftStr nvarchar(100);
	DECLARE @subquery nvarchar(100);
	DECLARE @query nvarchar(max);
	DECLARE @Results TABLE (ResultText NVARCHAR(100));

	SET @subquery ='SELECT lft FROM ' + @TableName + ' WITH (NOLOCK) WHERE USERID = ' + CONVERT(NVARCHAR(100), @ParentId)

    INSERT INTO @Results
    EXECUTE SP_EXECUTESQL @subquery

	set @myLeftStr  = (SELECT * FROM @Results)

	set @query = 'UPDATE ' + @TableName + ' SET rgt = rgt + 2 WHERE rgt > ' + @myLeftStr + ' '
	set @query = @query + 'UPDATE ' + @TableName + ' SET lft = lft + 2 WHERE lft > ' + @myLeftStr + ' '
	set @query = @query + 'INSERT INTO ' + @TableName + ' (USERID, lft, rgt, PARENTID, SPONSORID) VALUES (' + 
	                                                       CONVERT(NVARCHAR(100), @UserId) + ', ' + 
														   @myLeftStr + ' + 1, ' + 
														   @myLeftStr + ' + 2, ' + 
														   CONVERT(NVARCHAR(100), @ParentId) + ', ' +
														   CONVERT(NVARCHAR(100), @SponsorId)  + ') '

	EXECUTE sp_executesql @query

	COMMIT TRANSACTION

END

/****** Object:  StoredProcedure [dbo].[sp_GetSubTreeDepth]    Script Date: 10/22/2020 6:32:47 PM ******/
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

