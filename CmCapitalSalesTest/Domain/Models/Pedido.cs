using CmCapitalSalesAvaliacao.Infra.Enums;

namespace CmCapitalSalesAvaliacao.Domain.Models
{
    public class Pedido
    {
        public long CdPedido { get; set; }
        public long CdCliente { get; set; }
        public DateTime DtPedido { get; set; } 
        public PedidoStatusEnum PedidoStatusEnum { get; set; }
        public Cliente ClienteNavigation { get; set; }
        public ICollection<PedidoItem> PedidoItensNavigation { get; } = new List<PedidoItem>();
    }
}
