CREATE TABLE [dbo].[MultipleChoiceAnswers]
(
	[MultipleChoiceAnswersId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [QuestionId] UNIQUEIDENTIFIER NULL, 
    [Answer] NVARCHAR(250) NULL, 
    [AnswerNo] INT NULL, 
    [QuestionnaireId] UNIQUEIDENTIFIER NULL,
	CONSTRAINT  FK_QuestionnaireMultipleChoiceAnswers FOREIGN KEY ([QuestionnaireId]) REFERENCES Questionnaire([QuestionnaireId]),
	CONSTRAINT  FK_QuestionMultipleChoiceAnswers FOREIGN KEY ([QuestionId]) REFERENCES Questions([QuestionId])
)
