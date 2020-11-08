--DROP TABLE BinaryTree
CREATE TABLE BinaryTree (
        USERID INT NOT NULL UNIQUE,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

INSERT INTO BinaryTree (USERID, LFT, RGT, PARENTID) VALUES
(1,1,20,0),
(2,2,13,1),
(3,14,19,1),
(4,3,8,1),
(5,9,12,1),
(6,15,16,1),
(7,17,18,1),
(8,4,5,1),
(9,6,7,1),
(10,10,11,1);

SELECT * FROM BinaryTree WITH (NOLOCK);
EXEC sp_GetFullTreeSchema 'BinaryTree'

--UPDATE BinaryTree SET ISMASTERPARENT = 1 WHERE USERID = 1

--DROP TABLE Users ----------------------------------------------------
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


 SELECT * FROM USERS

 --RETRIEVE FULL TREE ----------------------------------------------------
SELECT node.USERID, NodeUser.USERNAME
FROM
BinaryTree AS node
JOIN Users NodeUser ON NodeUser.USERID = node.USERID,
BinaryTree AS parent
WHERE node.lft BETWEEN parent.lft AND parent.rgt AND parent.USERID = 1
ORDER BY node.lft;

 --RETRIEVE FULL TREE WITH DEPTH ----------------------------------------------------
SELECT node.USERID, NodeUser.USERNAME, node.SPONSORID
FROM
BinaryTree AS node
JOIN Users NodeUser ON NodeUser.USERID = node.USERID
JOIN (SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
		FROM 
		BinaryTree AS node,
		BinaryTree AS parent
		WHERE node.lft BETWEEN parent.lft AND parent.rgt
		GROUP BY node.USERID ) d ON d.USERID = node.USERID,
BinaryTree AS parent
WHERE node.lft BETWEEN parent.lft AND parent.rgt AND 
      parent.USERID = (SELECT USERID FROM BinaryTree WHERE ISMASTERPARENT = 1)
ORDER BY node.lft;

--FINDING LEAF NODES ----------------------------------------------------
SELECT b.USERID
FROM BinaryTree b
JOIN Users u ON u.USERID = b.USERID
WHERE b.rgt = b.lft + 1;

--RETRIEVING A SINGLE PATH TO TOPMOST PARENT ----------------------------------------------------
SELECT parent.USERID, ParentUser.USERNAME
FROM 
BinaryTree AS node,
BinaryTree AS parent
JOIN Users ParentUser ON ParentUser.USERID = parent.USERID
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.USERID = 1
ORDER BY parent.lft;

--FINDING THE DEPTH OF THE NODES ----------------------------------------------------
SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
FROM 
BinaryTree AS node,
BinaryTree AS parent
WHERE node.lft BETWEEN parent.lft AND parent.rgt
GROUP BY node.USERID
--ORDER BY node.lft;

--DEPTH OF A SUBTREE ----------------------------------------------------
SELECT node.USERID, (COUNT(parent.USERID) - (sub_tree.depth + 1)) AS depth
FROM 
BinaryTree AS node,
BinaryTree AS parent,
BinaryTree AS sub_parent,
(
                SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
                FROM BinaryTree AS node,
                BinaryTree AS parent
                WHERE node.lft BETWEEN parent.lft AND parent.rgt
                AND node.USERID = 1
                GROUP BY node.USERID
                --ORDER BY node.lft
)AS sub_tree
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.lft BETWEEN sub_parent.lft AND sub_parent.rgt
        AND sub_parent.USERID = sub_tree.USERID
GROUP BY node.USERID, sub_tree.depth, node.lft
ORDER BY node.lft;

--DEPTH OF A SUBTREE WITH LFT RGT ----------------------------------------------------
SELECT node.USERID, (COUNT(parent.USERID) - (sub_tree.depth + 1)) AS depth, node.LFT, node.RGT
FROM 
BinaryTree AS node,
BinaryTree AS parent,
BinaryTree AS sub_parent,
(
                SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
                FROM BinaryTree AS node,
                BinaryTree AS parent
                WHERE node.lft BETWEEN parent.lft AND parent.rgt
                AND node.USERID = 1
                GROUP BY node.USERID
                --ORDER BY node.lft
)AS sub_tree
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.lft BETWEEN sub_parent.lft AND sub_parent.rgt
        AND sub_parent.USERID = sub_tree.USERID
GROUP BY node.USERID, sub_tree.depth, node.lft, node.RGT
ORDER BY node.lft;

---SP---
EXEC dbo.sp_GetSubTreeDepth 1, 'BinaryTree'
EXEC dbo.sp_GetSubTreeDepth 2, 'ForcedMatrix3x3'
--------

--INSERT NODE AS A SIBLING ----------------------------------------------------

BEGIN TRANSACTION

DECLARE @myRight int; 

SELECT @myRight = rgt FROM BinaryTree
WHERE USERID = 5;

UPDATE BinaryTree SET rgt = rgt + 2 WHERE rgt > @myRight;
UPDATE BinaryTree SET lft = lft + 2 WHERE lft > @myRight;

INSERT INTO BinaryTree(USERID, lft, rgt) VALUES(11, @myRight + 1, @myRight + 2);

ROLLBACK TRANSACTION

COMMIT TRANSACTION

---SP---
--EXEC dbo.sp_InsertNode @UserId = 11, @ParentId = 5, @TableName = 'BinaryTree';
EXEC dbo.sp_InsertNode 3, 1, 'BinaryTree'
--------

--INSERT NODE AS A SINGLE LEAF NODE ----------------------------------------------------

BEGIN TRANSACTION

DECLARE @myLeft INT

SELECT @myLeft = lft FROM BinaryTree WHERE USERID = 6;

UPDATE BinaryTree SET rgt = rgt + 2 WHERE rgt > @myLeft;
UPDATE BinaryTree SET lft = lft + 2 WHERE lft > @myLeft;

INSERT INTO BinaryTree(USERID, lft, rgt) VALUES(13, @myLeft + 1, @myLeft + 2);

ROLLBACK TRANSACTION

COMMIT TRANSACTION

---SP---
--EXEC dbo.sp_InsertNode @UserId = 11, @ParentId = 5, @TableName = 'BinaryTree';
EXEC dbo.sp_InsertLeafNode 4, 2, 'BinaryTree'
SELECT * FROM BinaryTree WITH (NOLOCK);
--------



----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--FORCED MATRIX 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 INSERT INTO Users (USERNAME, FIRSTNAME, LASTNAME, SPONSORID, JOINDATE) VALUES
 ('kenpark', 'Ken', 'Park', null, 'October 1, 2020'),
 ('janesmith', 'Jane', 'Smith', 1, 'October 2, 2020'),
 ('ivyviola', 'Ivy', 'Viola', 1, 'October 3, 2020'),
 ('joshbailey', 'Josh', 'Bailey', 2, 'October 3, 2020'),
 ('duncantaylor', 'Duncan', 'Taylor', 2, 'October 4, 2020'),
 ('andersoncooper', 'Anderson', 'Cooper', 3, 'October 5, 2020'),
 ('griffinluna', 'Griffin', 'Luna', 4, 'October 6, 2020'),
 ('finleygray', 'Finley', 'Gray', 7, 'October 6, 2020'),
 ('jennywest', 'Jenny', 'West', 7, 'October 7, 2020'),
 ('shawavery', 'Shaw', 'Avery', 8, 'October 8, 2020'),
 ('jollybee', 'Jolly', 'Bee', 9, 'October 12, 2020'),
 ('mishaken', 'Misha', 'Ken', 5, 'October 14, 2020'),
 ('henryford', 'Henry', 'Ford', 6, 'October 15, 2020'),
 ('kenmiles', 'Ken', 'Miles', 6, 'October 15, 2020')

SELECT * FROM USERS

--DROP TABLE [dbo].[ForcedMatrix3x3]
CREATE TABLE ForcedMatrix3x3 (
        USERID INT NOT NULL,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

INSERT INTO ForcedMatrix3x3 (USERID, LFT, RGT) VALUES
(1,1,24,1),
(2,2,29,1),
(3,10,17,1),
(4,18,23,1),
(5,3,4,1),
(6,5,6,1),
(7,7,8,1),
(8,11,12,1),
(9,13,14,1),
(10,15,16,1),
(11,19,20,1),
(12,21,22,1)

UPDATE ForcedMatrix3x3 SET ISMASTERPARENT = 1 WHERE USERID = 1

SELECT * FROM ForcedMatrix3x3 WITH (NOLOCK)
EXEC sp_GetFullTreeSchema 'ForcedMatrix3x3'

--RETRIEVING A SINGLE PATH TO TOPMOST PARENT ----------------------------------------------------
SELECT parent.USERID, ParentUser.USERNAME
FROM 
ForcedMatrix3x3 AS node,
ForcedMatrix3x3 AS parent
JOIN Users ParentUser ON ParentUser.USERID = parent.USERID
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.USERID = 9
ORDER BY parent.lft;

--DEPTH OF A SUBTREE ----------------------------------------------------
SELECT node.USERID, (COUNT(parent.USERID) - (sub_tree.depth + 1)) AS depth
FROM 
ForcedMatrix3x3 AS node,
ForcedMatrix3x3 AS parent,
ForcedMatrix3x3 AS sub_parent,
(
                SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
                FROM ForcedMatrix3x3 AS node,
                ForcedMatrix3x3 AS parent
                WHERE node.lft BETWEEN parent.lft AND parent.rgt
                AND node.USERID = 4
                GROUP BY node.USERID
                --ORDER BY node.lft
)AS sub_tree
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.lft BETWEEN sub_parent.lft AND sub_parent.rgt
        AND sub_parent.USERID = sub_tree.USERID
GROUP BY node.USERID, sub_tree.depth, node.lft
ORDER BY node.lft;

 --RETRIEVE FULL TREE ----------------------------------------------------
SELECT node.USERID, NodeUser.USERNAME
FROM
ForcedMatrix3x3 AS node
JOIN Users NodeUser ON NodeUser.USERID = node.USERID,
ForcedMatrix3x3 AS parent
WHERE node.lft BETWEEN parent.lft AND parent.rgt AND parent.USERID = 1
ORDER BY node.lft;

--FINDING LEAF NODES ----------------------------------------------------
SELECT b.USERID
FROM BinaryTree b
JOIN Users u ON u.USERID = b.USERID
WHERE b.rgt = b.lft + 1;

--RETRIEVING A SINGLE PATH TO TOPMOST PARENT ----------------------------------------------------
SELECT parent.USERID, ParentUser.USERNAME
FROM 
BinaryTree AS node,
BinaryTree AS parent
JOIN Users ParentUser ON ParentUser.USERID = parent.USERID
WHERE node.lft BETWEEN parent.lft AND parent.rgt
        AND node.USERID = 9
ORDER BY parent.lft;

--FINDING THE DEPTH OF THE NODES ----------------------------------------------------
SELECT node.USERID, (COUNT(parent.USERID) - 1) AS depth
FROM 
BinaryTree AS node,
BinaryTree AS parent
WHERE node.lft BETWEEN parent.lft AND parent.rgt
GROUP BY node.USERID
--ORDER BY node.lft;
