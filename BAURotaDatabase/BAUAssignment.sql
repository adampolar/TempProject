CREATE TABLE [dbo].[BAUAssignment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1) ,
	[EmployeeNumber] INT NOT NULL,
	[Date] DATE NOT NULL,
	[Morning] BIT NOT NULL
)
