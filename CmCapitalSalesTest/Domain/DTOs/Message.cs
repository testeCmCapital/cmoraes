using CmCapitalSalesAvaliacao.Infra.Enums;

namespace CmCapitalSalesAvaliacao.Domain.DTO
{
    public class Message
    {
        public string Description { get; set; }
        public MessageStatusEnum MessageStatusEnum { get; set; }
    }
}
