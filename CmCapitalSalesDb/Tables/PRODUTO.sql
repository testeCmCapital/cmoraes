﻿CREATE TABLE [dbo].[PRODUTO]
(
	[CD_PRODUTO] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [DESCRICAO] VARCHAR(60) NOT NULL, 
    [DT_VENCIMENTO] DATETIME NOT NULL, 
    [DT_CADASTRO] DATETIME NOT NULL, 
    [VALOR_UNITARIO] MONEY NOT NULL
)
