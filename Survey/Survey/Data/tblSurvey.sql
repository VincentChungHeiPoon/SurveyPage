USE [CASASurvey]
GO

/****** Object:  Table [dbo].[tblSurvey]    Script Date: 8/29/2019 12:23:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSurvey](
	[tblSurveyID] [varchar](50) NOT NULL,
	[QuestionText] [varchar](1024) NOT NULL,
	[QuestionDescription] [varchar](1024) NOT NULL,
	[Active] [bit] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblSurvey] ADD  CONSTRAINT [DF_tblSurvey_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[tblSurvey] ADD  CONSTRAINT [DF_tblSurvey_Modified]  DEFAULT (getdate()) FOR [Modified]
GO

ALTER TABLE [dbo].[tblSurvey] ADD  CONSTRAINT [DF_tblSurvey_Created]  DEFAULT (getdate()) FOR [Created]
GO

