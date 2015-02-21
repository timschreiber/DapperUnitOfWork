USE [master]
GO

SET NOCOUNT ON
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'LosGatos')
BEGIN
	ALTER DATABASE LosGatos SET SINGLE_USER
	DROP DATABASE LosGatos
END

CREATE DATABASE LosGatos
GO

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'LosGatosUser')
BEGIN
	CREATE LOGIN [LosGatosUser] WITH PASSWORD = N'Password123', DEFAULT_DATABASE = [LosGatos],
		DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
	
	ALTER LOGIN [LosGatosUser] ENABLE
END
GO

USE [LosGatos]
GO

CREATE USER [LosGatosUser] FOR LOGIN [LosGatosUser]
GO

EXEC sp_addrolemember N'db_datareader', N'LosGatosUser'
EXEC sp_addrolemember N'db_datawriter', N'LosGatosUser'
GO

USE [LosGatos]
GO
/****** Object:  Table [dbo].[Breed]    Script Date: 2/21/2015 4:59:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Breed](
	[Breed] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Breed] PRIMARY KEY CLUSTERED 
(
	[Breed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cat]    Script Date: 2/21/2015 4:59:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cat](
	[CatId] [uniqueidentifier] NOT NULL,
	[BreedId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_Cat] PRIMARY KEY CLUSTERED 
(
	[CatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Breed] ADD  CONSTRAINT [DF_Breed_Breed]  DEFAULT (newid()) FOR [Breed]
GO
ALTER TABLE [dbo].[Cat] ADD  CONSTRAINT [DF_Cat_CatId]  DEFAULT (newid()) FOR [CatId]
GO
ALTER TABLE [dbo].[Cat]  WITH CHECK ADD  CONSTRAINT [FK_Cat_Breed] FOREIGN KEY([BreedId])
REFERENCES [dbo].[Breed] ([Breed])
GO
ALTER TABLE [dbo].[Cat] CHECK CONSTRAINT [FK_Cat_Breed]
GO
