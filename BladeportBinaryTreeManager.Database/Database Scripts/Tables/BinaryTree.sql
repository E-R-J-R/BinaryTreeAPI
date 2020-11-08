CREATE TABLE BinaryTree (
        USERID INT NOT NULL UNIQUE,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

--INSERT INTO BinaryTree (USERID, LFT, RGT, PARENTID) VALUES
--(1,1,20,0),
--(2,2,13,1),
--(3,14,19,1),
--(4,3,8,1),
--(5,9,12,1),
--(6,15,16,1),
--(7,17,18,1),
--(8,4,5,1),
--(9,6,7,1),
--(10,10,11,1);