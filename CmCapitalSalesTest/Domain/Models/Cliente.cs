namespace CmCapitalSalesAvaliacao.Domain.Models
{
    public class Cliente
    {
        public int CdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DtUltimaCompra { get; set; }
        public decimal Saldo { get; set; }

    }
}
