using CmCapitalSalesAvaliacao.Domain.DTOs;
using CmCapitalSalesAvaliacao.Infra.Enums;

namespace CmCapitalSalesAvaliacao.Domain.Services
{
    public abstract class Service
    {
        public JsonReturn TratarRetornoExcecao(JsonReturn returnData, string message)
        {
            returnData.IsSucess = false;
            
            returnData.Messages = new List<Message>
            {
                new Message { Description = message, MessageStatusEnum = MessageStatusEnum.Error }
            };

            return returnData;
        }

    }
}
