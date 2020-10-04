﻿CREATE TABLE [dbo].[Cidade]
(
	[CodIBGE] INT NOT NULL PRIMARY KEY,
	[Nome] VARCHAR(55) NOT NULL,
	[Latitude] VARCHAR(20) NOT NULL,
	[Longitude] VARCHAR(20) NOT NULL,
	[UF] INT NOT NULL ,
	[Regiao] INT NOT NULL,	
	FOREIGN KEY ([UF]) REFERENCES [dbo].[UF] (ID),
	FOREIGN KEY ([Regiao]) REFERENCES [dbo].[Regiao] (ID),
	CONSTRAINT UN_NOME_UF UNIQUE([Nome],[UF])
);
