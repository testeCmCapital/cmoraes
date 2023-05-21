using CmCapitalSalesAvaliacao.Domain.Models;

namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class PedidoDTO
    {
        

        public long CdPedido { get; set; }

        public long CdCliente { get; set; }
        public List<PedidoItemDTO> PedidoItensDTO { get; set; }
        public DateTime DtPedido { get; set; }
    }
}
