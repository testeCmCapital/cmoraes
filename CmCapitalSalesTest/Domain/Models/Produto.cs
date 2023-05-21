namespace CmCapitalSalesAvaliacao.Domain.Models
{
    public class Produto
    {
        public int CdProduto { get; set; }
        public int Descricao { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtVencimento { get; set; }
        public decimal ValorUnitario { get; set; }

    }
}
