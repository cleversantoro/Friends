USE [master]
GO

SET NOCOUNT ON
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'FriendsDB')
--BEGIN
--	-ALTER DATABASE FriendsDB SET SINGLE_USER
DROP DATABASE FriendsDB
--END
CREATE DATABASE FriendsDB
GO

--IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'Friends')
--BEGIN
--	CREATE LOGIN Friends WITH PASSWORD = N'M0t0r0l401', DEFAULT_DATABASE = FriendsDB,DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
--	ALTER LOGIN Friends ENABLE
--END
--GO
--CREATE USER Friends FOR LOGIN Friends
--GO

--EXEC sp_addrolemember N'db_datareader', N'LosGatosUser'
--EXEC sp_addrolemember N'db_datawriter', N'LosGatosUser'
GO

USE [FriendsDB]
GO
/****** Object:  Table [dbo].[Breed]    Script Date: 2/22/2015 10:09:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CalculoHistoricoLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Contato] [int] NULL,
	[id_Amigo] [int] NULL,
	[Distancia] [nvarchar](20) NULL,
	[Data_Insercao] [datetime] NULL,
CONSTRAINT [PK_CalculoHistoricoLog] PRIMARY KEY CLUSTERED ([id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CalculoHistoricoLog] ADD  CONSTRAINT [DF_CalculoHistoricoLog_Data_Insercao]  DEFAULT (getdate()) FOR [Data_Insercao]
GO


CREATE TABLE [dbo].[Enderecos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Logradouro] [nvarchar](150) NULL,
	[Numero] [int] NULL,
	[Cidade] [nvarchar](250) NULL,
	[Estado] [nvarchar](100) NULL,
	[Pais] [nvarchar](100) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Contatos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Endereco] [int] NOT NULL,
	[Nome] [nvarchar](100) NULL,
	[Sobrenome] [nvarchar](250) NULL,
CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contatos]  WITH CHECK ADD  CONSTRAINT [FK_Person_Address] FOREIGN KEY([id_Endereco])
REFERENCES [dbo].[Enderecos] ([id])
GO
ALTER TABLE [dbo].[Contatos] CHECK CONSTRAINT [FK_Person_Address]
GO

CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](250) NULL,
	[Sobrenome] [nvarchar](250) NULL,
	[UserName] [nvarchar](150) NULL,
	[PassWord] [nvarchar](100) NULL,
	[Token] [nvarchar](100) NULL,
	[Ultimo_Acesso] [datetime] NULL,
CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO

           
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Cafelandia', 412, 'Santo André', 'São Paulo', 'Brasil', '-23.6958763','-46.5287875') 
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua dos Bambus', 521, 'Santo André', 'São Paulo', 'Brasil','-23.6902945', '-46.5135581')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Coréia', 430, 'Santo André', 'São Paulo', 'Brasil','-23.6387126','-46.5119170' )
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Carlos Lacerda',  25, 'São Bernardo do Campo', 'São Paulo', 'Brasil','-23.7195112','-46.5406236')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Andre Coppini' , 29, 'Sao Bernardo Do Campo' , 'Sao Paulo', 'Brasil', '-23.7175365','-46.5404069')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Machado De Assis',  73, 'Sao Bernardo Do Campo' , 'Sao Paulo', 'Brasil','-23.7178678','-46.5422826')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Rosa Margonari Borali' , 41, 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7187546','-46.5404101')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Americo Margonari' , 46, 'São Bernardo do Campo' , 'Sao Paulo' , 'Brasil','-23.7116071','-46.5477997')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Ângelo Picoli' , 80 , 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7116071','-46.5477997')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Marte' , 7, 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7244504','-46.5268659')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua das Folhas' , 53, 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7277518','-46.5282141')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Passagem Daniela Peres' , 50, 'Sao Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7128962','-46.5329595')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua Marechal Deodoro', 1932, 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7150778','-46.5512778')
INSERT INTO [dbo].[Enderecos]  ([Logradouro],[Numero],[Cidade],[Estado],[Pais],[Latitude],[Longitude]) values ('Rua bela vista', 724, 'São Bernardo do Campo' , 'Sao Paulo', 'Brasil','-23.7153467','-46.5480427')
GO

INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(1,'Valeria','Barros')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(2,'Cleber','Santos')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(3,'Barbara','Silva')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(4,'Claudio','Rodrigues')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(5,'Lucien' ,'Silva')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(6,'Vanderlei' ,'Leite')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(7,'Leonardo' ,'Rodrigues')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(8,'Alexandre' ,'Refineti')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(9,'Rosana' ,'Almeida')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(10,'Claudio' ,'Jose')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(11,'Cristina' ,'Isidoro')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(12,'Aline' ,'Carvalho')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(13,'Anderson' ,'Valentim')
INSERT INTO [dbo].[Contatos] ([id_Endereco],[Nome],[Sobrenome]) VALUES(14,'Everton','Santana')
GO

INSERT INTO [dbo].[Usuarios] ([Nome],[Sobrenome],[UserName],[PassWord],[Token]) VALUES ('Administrador','Admin','admin','admin123','')
GO


