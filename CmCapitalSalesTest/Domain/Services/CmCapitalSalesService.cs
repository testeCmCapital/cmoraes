using CmCapitalSalesAvaliacao.Domain.DTOs;
using CmCapitalSalesAvaliacao.Infra.Data;
using CmCapitalSalesAvaliacao.Infra.Enums;
using Microsoft.EntityFrameworkCore;

namespace CmCapitalSalesAvaliacao.Domain.Services
{
    public class CmCapitalSalesService : Service
    {
        private readonly CmCapitalSalesDbContext _context;
        public CmCapitalSalesService(CmCapitalSalesDbContext context)
        {
            _context = context;
        }
        public JsonReturn EfetivarPedido(PedidoDTO PedidoDTO)
        {
            var returnData = new JsonReturn { IsSucess = true };

            try
            {
                var cliente = _context.Cliente.FirstOrDefault(c => c.CdCliente == PedidoDTO.CdCliente);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado");


                var saldoTotal = cliente.Saldo;
                var saldoDeCompra = saldoTotal * 80/100;

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
                var pedido = _context.Pedido
                                .Include(c => c.PedidoItem)
                                .Include(c => c.CdClienteNavigation)
                                .FirstOrDefault(c => c.CdPedido == CdPedido);

                if(pedido == null)
                    throw new Exception("Pedido não encontrado");

                pedido.CdClienteNavigation.Saldo += pedido.PedidoItem.Sum(c => c.ValorTotal);

                pedido.Status = (int) PedidoStatusEnum.Cancelado;

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
                    from p in _context.Produto 
                    join pi in _context.PedidoItem on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.NrQuantidade, p.ValorUnitario }).ToList();

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
                    from p in _context.Produto
                    join pi in _context.PedidoItem on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.NrQuantidade, p.ValorUnitario }).ToList();

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
                var cliente = _context.Cliente.FirstOrDefault(c => c.CdCliente == CdCliente);

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }
        }
        
    }
}
