namespace CmCapitalSalesAvaliacao.Domain.Models
{
    public class PedidoItem
    {
        public long CdPedidoItem { get; set; }
        public long CdPedido { get; set; }
        public int CdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public Pedido PedidoNavigation { get; set; }
    }
}
