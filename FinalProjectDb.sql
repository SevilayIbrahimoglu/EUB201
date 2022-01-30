USE [EUB201]
GO

/****** Object:  Table [dbo].[noteList]    Script Date: 30.01.2022 21:34:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[noteList](
	[noteId] [int] IDENTITY(1000,1) NOT NULL,
	[noteTitle] [varchar](50) NOT NULL,
	[note] [varchar](max) NOT NULL,
	[createTime] [varchar](30) NOT NULL,
	[lastUpdateTime] [varchar](30) NOT NULL,
 CONSTRAINT [PK_noteList] PRIMARY KEY CLUSTERED 
(
	[noteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

