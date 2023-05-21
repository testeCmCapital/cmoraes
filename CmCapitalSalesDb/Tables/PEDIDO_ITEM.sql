﻿CREATE TABLE [dbo].[PEDIDO_ITEM]
(
	[CD_PEDIDO_ITEM] BIGINT NOT NULL, 
    [CD_PEDIDO] BIGINT NOT NULL, 
    [CD_PRODUTO] INT NOT NULL, 
    [NR_QUANTIDADE] INT NOT NULL, 
    [VALOR_TOTAL] MONEY NOT NULL,
    
    CONSTRAINT PK_PEDIDO_ITEM PRIMARY KEY NONCLUSTERED ([CD_PEDIDO_ITEM]),
    CONSTRAINT FK_PEDIDO_ITEM_PEDIDO FOREIGN KEY ([CD_PEDIDO]) REFERENCES PEDIDO([CD_PEDIDO]),
    CONSTRAINT FK_PEDIDO_ITEM_PRODUTO FOREIGN KEY ([CD_PRODUTO]) REFERENCES PRODUTO([CD_PRODUTO])
)