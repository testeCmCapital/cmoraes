﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CmCapitalSalesAvaliacao.Domain.Models
{
    public partial class PedidoItem
    {
        public long CdPedidoItem { get; set; }
        public long CdPedido { get; set; }
        public int CdProduto { get; set; }
        public int NrQuantidade { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Pedido CdPedidoNavigation { get; set; }
        public virtual Produto CdProdutoNavigation { get; set; }
    }
}