CREATE TABLE [dbo].[Table]
(
	[First] VARCHAR(50) NULL , 
    [Last] VARCHAR(50) NULL, 
    [Mobile] VARCHAR(50) NOT NULL, 
    [Email] VARBINARY(50) NULL, 
    [Catagory] VARBINARY(50) NULL, 
    PRIMARY KEY ([Mobile])
)
