namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class JsonReturn
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
