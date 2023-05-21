using CmCapitalSalesAvaliacao.Infra.Enums;

namespace CmCapitalSalesAvaliacao.Domain.DTOs
{
    public class Message
    {
        public string Description { get; set; }
        public MessageStatusEnum MessageStatusEnum { get; set; }
    }
}
