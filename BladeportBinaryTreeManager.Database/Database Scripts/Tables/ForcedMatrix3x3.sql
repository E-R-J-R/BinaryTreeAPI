CREATE TABLE ForcedMatrix3x3 (
        USERID INT NOT NULL,
        LFT INT NOT NULL,
        RGT INT NOT NULL,
		PARENTID INT NOT NULL,
		SPONSORID INT NULL,
		ISMASTERPARENT BIT default 'FALSE'
);

--INSERT INTO ForcedMatrix3x3 (USERID, LFT, RGT) VALUES
--(1,1,24,1),
--(2,2,29,1),
--(3,10,17,1),
--(4,18,23,1),
--(5,3,4,1),
--(6,5,6,1),
--(7,7,8,1),
--(8,11,12,1),
--(9,13,14,1),
--(10,15,16,1),
--(11,19,20,1),
--(12,21,22,1)