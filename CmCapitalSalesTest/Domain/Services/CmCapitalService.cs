using CmCapitalSalesAvaliacao.Domain.DTO;
using CmCapitalSalesAvaliacao.Infra.Data;

namespace CmCapitalSalesAvaliacao.Domain.Services
{
    public class CmCapitalService
    {
        private readonly CmCapitalSalesContext _context;
        public CmCapitalService(CmCapitalSalesContext context)
        {
            _context = context;
        }
        public JsonReturn EfetivarPedido()
        {
            var retorno = new JsonReturn { IsSucess = true };
            return retorno;

        }
    }
}
