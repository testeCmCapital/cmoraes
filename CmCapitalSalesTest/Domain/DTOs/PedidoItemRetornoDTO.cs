namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class PedidoItemRetornoDTO
    {
        public int CdProduto { get; set; }
        public int NrQuantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorLucroAcumulado { get; set; }

    }
}
