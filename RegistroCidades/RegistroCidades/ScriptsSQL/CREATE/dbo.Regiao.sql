﻿CREATE TABLE [dbo].[Regiao]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Descricao] VARCHAR(55) NOT NULL,
	[UF] INT NOT NULL,	
	FOREIGN KEY ([UF]) REFERENCES [dbo].[UF] (ID),
		
);