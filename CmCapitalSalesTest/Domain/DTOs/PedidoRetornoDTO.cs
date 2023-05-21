using CmCapitalSalesAvaliacao.Domain.Models;

namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class PedidoRetornoDTO
    {
        public long CdPedido { get; set; }

        public long CdCliente { get; set; }
        public List<PedidoItemDTO> PedidoItensDTO { get; set; }
        public DateTime DtPedido { get; set; }
        
        private PedidoRetornoDTO() { }

        public PedidoRetornoDTO (Pedido Pedido)
        {
            var pedidoRetornoDto = new PedidoRetornoDTO
            {
                CdCliente = Pedido.CdCliente,
                CdPedido = Pedido.CdPedido,
                PedidoItensDTO = new List<PedidoItemDTO>()
            };

            foreach (var pedidoItem in Pedido.PedidoItensNavigation)
            {
                var pedidoItemDto = new PedidoItemDTO
                {
                    CdProduto = pedidoItem.CdProduto,
                    NrQuantidade = pedidoItem.Quantidade,
                    ValorTotal = pedidoItem.ValorTotal
                };
            }

        }
    }
}
