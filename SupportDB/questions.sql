CREATE TABLE [dbo].[questions]
(
	[QuestionId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Question] NVARCHAR(250) NULL, 
    [QuestionNo] INT NULL
)
