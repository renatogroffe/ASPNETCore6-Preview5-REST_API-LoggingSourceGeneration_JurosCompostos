using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIFinancas.Models;
using APIFinancas.Logging;

namespace APIFinancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculoFinanceiroController : ControllerBase
    {
        private readonly ILogger<CalculoFinanceiroController> _logger;

        public CalculoFinanceiroController(ILogger<CalculoFinanceiroController> logger)
        {
            _logger = logger;
        }

        [HttpGet("juroscompostos")]
        [ProducesResponseType(typeof(Emprestimo), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FalhaCalculo), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Emprestimo> Get(
            double valorEmprestimo, int numMeses, double percTaxa)
        {
            // Exemplo de uso de compile-time source generator
            _logger.LogNovaRequisicaoCalculo(valorEmprestimo, numMeses, percTaxa);

            if (valorEmprestimo <= 0)
                return GerarResultParamInvalido("Valor do Empréstimo");

            if (numMeses <= 0)
                return GerarResultParamInvalido("Número de Meses");

            if (percTaxa <= 0)
                return GerarResultParamInvalido("Percentual da Taxa de Juros");

            double valorFinalJuros =
                CalculoFinanceiro.CalcularValorComJurosCompostos(
                    valorEmprestimo, numMeses, percTaxa);
            
            // Exemplo de uso de compile-time source generator
            _logger.LogValorComJuros(valorFinalJuros);

            return new Emprestimo()
            {
                ValorEmprestimo = valorEmprestimo,
                NumMeses = numMeses,
                TaxaPercentual = percTaxa,
                ValorFinalComJuros = valorFinalJuros
            };
        }

        private BadRequestObjectResult GerarResultParamInvalido(
            string nomeCampo)
        {
            // Exemplo de uso de compile-time source generator
            _logger.LogParametroInvalido(nomeCampo);
            
            return new BadRequestObjectResult(
                new FalhaCalculo() { Mensagem = $"{nomeCampo} deve ser maior do que zero!" });
        }
    }
}