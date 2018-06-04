CREATE TABLE [dbo].[Questionnaire]
(
	[QuestionnaireId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [QuestionnaireDate] DATETIME NULL DEFAULT getdate()
)
