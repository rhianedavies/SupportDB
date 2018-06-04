CREATE TABLE [dbo].[FreeTextAnswers]
(
	[FreeTextAnswersId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [QuestionId] UNIQUEIDENTIFIER NULL, 
    [Answer] VARCHAR(250) NULL, 
	CONSTRAINT  FK_QuestionFreeTextAnswers FOREIGN KEY ([QuestionId]) REFERENCES Questions([QuestionId])
)
