using CmCapitalSalesAvaliacao.Domain.DTO;
using CmCapitalSalesAvaliacao.Domain.DTOs;
using CmCapitalSalesAvaliacao.Infra.Data;
using CmCapitalSalesAvaliacao.Infra.Enums;
using Microsoft.EntityFrameworkCore;

namespace CmCapitalSalesAvaliacao.Domain.Services
{
    public class CmCapitalService : Service
    {
        private readonly CmCapitalSalesContext _context;
        public CmCapitalService(CmCapitalSalesContext context)
        {
            _context = context;
        }
        public JsonReturn EfetivarPedido(PedidoDTO PedidoDTO)
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.CdCliente == PedidoDTO.CdCliente);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado");

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }

        }

        public JsonReturn CancelarPedido(int CdPedido)
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var pedido = _context.Pedidos
                                .Include(c => c.PedidoItensNavigation)
                                .Include(c => c.ClienteNavigation)
                                .FirstOrDefault(c => c.CdPedido == CdPedido);

                if(pedido == null)
                    throw new Exception("Pedido não encontrado");

                pedido.ClienteNavigation.Saldo += pedido.PedidoItensNavigation.Sum(c => c.ValorTotal);

                pedido.PedidoStatusEnum = PedidoStatusEnum.Cancelado;

                _context.SaveChanges();

                returnData.Data = new { CdPedido };
                
                returnData.Messages = new List<Message>
                {
                    new Message { Description = "Pedido Cancelado com Sucesso", MessageStatusEnum = MessageStatusEnum.Info }
                };

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }

        }

        public JsonReturn ListarProdutosMaisVendidos()
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var produtosMaisVendidos =  (
                    from p in _context.Produtos 
                    join pi in _context.PedidoItens on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.Quantidade, p.ValorUnitario }).ToList();

                returnData.Data = produtosMaisVendidos;

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }

        }

        public JsonReturn ListarProdutosMenosVendidos()
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var produtosMenosVendidos = (
                    from p in _context.Produtos
                    join pi in _context.PedidoItens on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.Quantidade, p.ValorUnitario }).ToList();

                returnData.Data = produtosMenosVendidos;

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }
        }

        public JsonReturn ListarProdutosCompradosPorCliente(int CdCliente)
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.CdCliente == CdCliente);

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }
        }
        
    }
}
