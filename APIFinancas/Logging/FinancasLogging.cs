using Microsoft.Extensions.Logging;

namespace APIFinancas.Logging
{
    public static partial class FinancasLogging
    {
        [LoggerMessage(EventId = 1, Level = LogLevel.Information,
            Message = "Recebida nova requisição|" +
                "Valor do empréstimo: {valorEmprestimo}|" +
                "Número de meses: {numMeses}|" +
                "% Taxa de Juros: {percTaxa}")]
        public static partial void LogNovaRequisicaoCalculo(
            this ILogger logger, double valorEmprestimo, int numMeses, double percTaxa);

        [LoggerMessage(EventId = 2, Level = LogLevel.Information,
            Message = "Valor Final com Juros: {valorFinalJuros}")]
        public static partial void LogValorComJuros(
            this ILogger logger, double valorFinalJuros);

        [LoggerMessage(EventId = 3, Level = LogLevel.Error,
            Message = "O '{nomeCampo}' deve ser maior do que zero!")]
        public static partial void LogParametroInvalido(
            this ILogger logger, string nomeCampo);
    }
}