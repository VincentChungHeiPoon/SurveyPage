USE [CASASurvey]
GO

/****** Object:  Table [dbo].[tblSurveyAnswers]    Script Date: 8/29/2019 12:24:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSurveyAnswers](
	[tblUser_ID] [int] NOT NULL,
	[tblSurvey_ID] [int] NOT NULL,
	[UserAnswer] [varchar](1024) NULL,
	[OptOut] [bit] NOT NULL
) ON [PRIMARY]
GO

