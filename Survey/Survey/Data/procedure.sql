CREATE PROCEDURE procedureSelectQuestion @UserID varchar(100) AS
BEGIN
SELECT S.tblSurveyID, S.QuestionText, S.QuestionDescription, S.Active, S.Modified, S.Created
FROM tblSurvey S
WHERE S.tblSurveyID NOT IN (SELECT tblSurvey_ID
							FROM tblSurveyAnswers
							WHERE tblUser_ID = @UserID)
END;

-- EXEC procedureSelectQuestion parem;

CREATE PROCEDURE procedureSelectAllQuestion AS
BEGIN
SELECT *
FROM tblSurvey
END;

-- EXEC procedureSelectAllQuestion;

CREATE PROCEDURE [dbo].[procedureInsertAnswer] @UserID  VARCHAR(100), @SurveyID VARCHAR(1024), @Responds VARCHAR(1024), @OptOut bit AS
BEGIN
INSERT INTO tblSurveyAnswers(tblUser_ID, tblSurvey_ID, UserAnswer, OptOut)
VALUES(@UserID, @SurveyID,@Responds, 0 )
END;

CREATE PROCEDURE [dbo].[procedureInsertAnswerOptOut] @UserID  VARCHAR(100), @SurveyID VARCHAR(1024), @Responds VARCHAR(1024), @OptOut bit AS
BEGIN
INSERT INTO tblSurveyAnswers(tblUser_ID, tblSurvey_ID, UserAnswer, OptOut)
VALUES(@UserID, @SurveyID,NULL, 1)
END;
