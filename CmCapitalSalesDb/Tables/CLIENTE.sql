﻿CREATE TABLE [dbo].[CLIENTE]
(
	[CD_CLIENTE] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [NOME] VARCHAR(80) NOT NULL, 
    [DT_ULTIMA_COMPRA] DATETIME NULL, 
    [SALDO] MONEY NOT NULL

)
