namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class ProdutoElegivelDTO
    {
        public long CdProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public List<ValorLucroDTO> ValorLucroAnualAcumulado { get; set; }
        public DateTime DtVencimento { get; set; }
    }
}
