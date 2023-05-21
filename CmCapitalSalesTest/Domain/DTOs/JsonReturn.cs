namespace CmCapitalSalesAvaliacao.Domain.DTO
{
    public class JsonReturn
    {
        public object Data { get; set; }
        public bool IsSucess { get; set; }
        public List<Message> Messages { get; set; }
    }
}
