namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class PedidoDTO
    {
        public int CdCliente { get; set; }
        public List<int> CdProduto { get; set; }
        public DateTime DtPedido { get; set; }
    }
}
