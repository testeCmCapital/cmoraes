using CmCapitalSalesAvaliacao.Domain.DTOs;
using CmCapitalSalesAvaliacao.Domain.Models;
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

        public JsonReturn ListarProdutos()
        {
            var returnData = new JsonReturn { IsSuccess = true };

            try
            {
                var produtos = _context.Produto.ToList();

                returnData.Data = produtos;

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }
        }

        public JsonReturn EfetivarPedido(PedidoDTO PedidoDTO)
        {
            var returnData = new JsonReturn { IsSuccess = true };

            try
            {
                var cliente = _context.Cliente.FirstOrDefault(c => c.CdCliente == PedidoDTO.CdCliente);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado");

                var saldoDeCompra = CalcularSaldoCompra(cliente.Saldo);

                var produtosPedido = _context.Produto.ToList();

                var produtoPrecoPedido = (
                    from pd in produtosPedido
                    join pi in PedidoDTO.PedidoItensDTO on pd.CdProduto equals pi.CdProduto
                    select new { pd.CdProduto, pi.NrQuantidade, pd.ValorUnitario, pd.DtVencimento });

                if(!produtoPrecoPedido.Any())
                    throw new Exception("Produtos não encontrados");


                if (saldoDeCompra <= produtoPrecoPedido.Sum(p => p.ValorUnitario * p.NrQuantidade))
                {
                    var produtosElegiveis = (
                        from p in _context.Produto
                        where p.ValorUnitario <= saldoDeCompra
                        && p.DtVencimento <= produtosPedido.First().DtVencimento.AddMonths(-4)
                        select p ).Take(3);

                    if (!produtosElegiveis.Any())
                    {
                        throw new Exception("Saldo insuficiente para os produtos selecionados.");
                    }

                    var produtosElegiveisDto = new List<ProdutoElegivelDTO>();

                    foreach (var produtoElegivel in produtosElegiveis)
                    {
                        var taxaProduto = _context.Taxa.FirstOrDefault(tx => produtoElegivel.ValorUnitario  > tx.ValorInicial && produtoElegivel.ValorUnitario < tx.ValorFinal);

                        var produtoElegivelDto = new ProdutoElegivelDTO
                        {
                            CdProduto = produtoElegivel.CdProduto,
                            ValorUnitario = produtoElegivel.ValorUnitario,
                            DtVencimento = produtoElegivel.DtVencimento,
                            ValorLucroAnualAcumulado = new List<ValorLucroDTO>()
                        };

                        var dtPeriodo = DateTime.Now;
                        
                        while(produtoElegivel.DtVencimento.Year < dtPeriodo.Year)
                        {
                            var periodo = dtPeriodo.Year - DateTime.Now.Year;
                            var valorLucroAnual = taxaProduto == null ? (produtoElegivel.ValorUnitario * (decimal)taxaProduto.Percentual * periodo) / 100 : 0;

                            var valorLucroDto = new ValorLucroDTO
                            {
                                ValorJurosAcumulado = valorLucroAnual,
                                PeriodoJuros = dtPeriodo
                            };

                            produtoElegivelDto.ValorLucroAnualAcumulado.Add(valorLucroDto);
                            dtPeriodo.AddYears(1);


                        }

                        produtosElegiveisDto.Add(produtoElegivelDto);
                    }

                    returnData.Data = produtosElegiveisDto;


                    throw new Exception("Saldo insuficiente para os produtos selecionados. Mas temos Sugestões de novos produtos para você");
                }
                

                var pedido = new Pedido
                {
                    CdCliente = PedidoDTO.CdCliente,
                    Status = (int)PedidoStatusEnum.Efetuado,
                    PedidoItem = new List<PedidoItem>(),
                    DtPedido = DateTime.Now
                };

                foreach (var produtoPedido in produtoPrecoPedido)
                {
                    var somaProdutoPedido = produtoPedido.ValorUnitario * produtoPedido.NrQuantidade;
                    var pedidoItem = new PedidoItem
                    {
                        CdProduto = produtoPedido.CdProduto,
                        NrQuantidade = produtoPedido.NrQuantidade,
                        ValorTotal = somaProdutoPedido
                    };

                    cliente.Saldo -= somaProdutoPedido;
                    pedido.PedidoItem.Add(pedidoItem);
                }
                
                cliente.DtUltimaCompra = pedido.DtPedido;

                _context.Add(pedido);
                _context.Update(cliente);
                _context.SaveChanges();

                var pedidoRetorno = new PedidoRetornoDTO(pedido);


                returnData.Data = pedidoRetorno;

                returnData.Messages = new List<Message>
                {
                    new Message { Description = "Pedido Efetuado com Sucesso", MessageStatusEnum = MessageStatusEnum.Info }
                };

                return returnData;

            }
            catch (Exception e)
            {
                return TratarRetornoExcecao(returnData, e.Message);
            }

        }

        public JsonReturn CancelarPedido(int CdPedido)
        {
            var returnData = new JsonReturn { IsSuccess = true };

            try
            {
                var pedido = _context.Pedido
                                .Include(c => c.PedidoItem)
                                .Include(c => c.CdClienteNavigation)
                                .FirstOrDefault(c => c.CdPedido == CdPedido);

                if(pedido == null)
                    throw new Exception("Pedido não encontrado");

                if (!PedidoElegivelCancelamento(pedido))
                    throw new Exception("Pedido não elegivel para cancelamento");

                pedido.CdClienteNavigation.Saldo += pedido.PedidoItem.Sum(c => c.ValorTotal);
                pedido.Status = (int) PedidoStatusEnum.Cancelado;

                _context.Update(pedido);
                _context.SaveChanges();

                returnData.Data = new { CdPedido };
                
                returnData.Messages = new List<Message>
                {
                    new Message { Description = "Pedido Cancelado e saldo estornado com Sucesso", MessageStatusEnum = MessageStatusEnum.Info }
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
            var returnData = new JsonReturn { IsSuccess = true };

            try
            {
                var produtosMaisVendidos =  (
                    from p in _context.Produto 
                    join pi in _context.PedidoItem on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.NrQuantidade, p.ValorUnitario }).AsEnumerable();


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
            var returnData = new JsonReturn { IsSuccess = true };

            try
            {
                var produtosMenosVendidos = (
                    from p in _context.Produto
                    join pi in _context.PedidoItem on p.CdProduto equals pi.CdProduto
                    select new { pi.CdProduto, p.Descricao, pi.NrQuantidade, p.ValorUnitario }).AsEnumerable();

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
            var returnData = new JsonReturn { IsSuccess = true };

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

        private decimal CalcularSaldoCompra(decimal saldoTotal)
        {
            return saldoTotal * 80 / 100;
        }

        private bool PedidoElegivelCancelamento(Pedido Pedido)
        {
            return DateTime.Now <= Pedido.DtPedido.AddDays(7) && Pedido.Status == (int) PedidoStatusEnum.Efetuado;
        }

    }
}
