﻿CREATE TABLE [dbo].[PEDIDO]
(
	[CD_PEDIDO] BIGINT NOT NULL PRIMARY KEY, 
    [CD_CLIENTE] BIGINT NOT NULL, 
    [DT_PEDIDO] DATETIME NOT NULL, 
    [STATUS] TINYINT NOT NULL
)
