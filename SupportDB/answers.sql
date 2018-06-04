CREATE TABLE [dbo].[Answers]
(
	[AnswerId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [QuestionnaireId] UNIQUEIDENTIFIER NULL, 
    [QuestionId] UNIQUEIDENTIFIER NULL, 
    [MultipleChoiceAnswersId] UNIQUEIDENTIFIER NULL, 
    [FreeTextAnswersId] UNIQUEIDENTIFIER NULL,
	CONSTRAINT  FK_QuestionnaireAnswers FOREIGN KEY ([QuestionnaireId]) REFERENCES Questionnaire([QuestionnaireId]),
	CONSTRAINT  FK_QuestionAnswers FOREIGN KEY ([QuestionId]) REFERENCES Questions([QuestionId]),
	CONSTRAINT  FK_MultipleChoiceAnswersAnswers FOREIGN KEY ([MultipleChoiceAnswersId]) REFERENCES MultipleChoiceAnswers([MultipleChoiceAnswersId]),
	CONSTRAINT  FK_FreeTextAnswersAnswers FOREIGN KEY ([FreeTextAnswersId]) REFERENCES FreeTextAnswers([FreeTextAnswersId])
)
