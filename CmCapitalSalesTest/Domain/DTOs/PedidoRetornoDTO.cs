using CmCapitalSalesAvaliacao.Domain.Models;

namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class PedidoRetornoDTO
    {
        public long CdPedido { get; set; }

        public long CdCliente { get; set; }
        public List<PedidoItemRetornoDTO> PedidoItensRetornoDTO { get; set; }
        public DateTime DtPedido { get; set; }
        

        private PedidoRetornoDTO() { }

        public PedidoRetornoDTO (Pedido Pedido)
        {
            PedidoItensRetornoDTO = new List<PedidoItemRetornoDTO>();

            CdPedido = Pedido.CdPedido;
            CdCliente = Pedido.CdCliente;
            PedidoItensRetornoDTO = new List<PedidoItemRetornoDTO>();

            foreach (var pedidoItem in Pedido.PedidoItem)
            {
                var pedidoItemRetornoDto = new PedidoItemRetornoDTO
                {
                    CdProduto = pedidoItem.CdProduto,
                    NrQuantidade = pedidoItem.NrQuantidade,
                    ValorTotal = pedidoItem.ValorTotal,
                };


                PedidoItensRetornoDTO.Add(pedidoItemRetornoDto);
            }

        }
    }
}
