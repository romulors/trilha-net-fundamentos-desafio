using DesafioFundamentos.View;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        // Alteração: propriedades públicas somente leitura atribuídas no construtor
        public decimal PrecoInicial { get; }
        public decimal PrecoPorHora { get; }

        // passa a armazenar Veiculo em vez de string
        private readonly List<Veiculo> _veiculos = new();
        public IReadOnlyList<Veiculo> Veiculos => _veiculos.AsReadOnly();
        public readonly Historico Historico;

        public Estacionamento(decimal precoInicial, decimal precoPorHora, Historico historico = null)
        {
            PrecoInicial = precoInicial;
            PrecoPorHora = precoPorHora;

            // TODO: possibilitar salvar e recuperar o histórico de alguma forma
            Historico = historico ?? new Historico();
        }

        public void AdicionarVeiculo()
        {
            Placa placa = ConsoleUI.SolicitarPlacaParaEstacionar();
            if (VerificarSeVeiculoExiste(placa))
            {
                ConsoleUI.ExibirVeiculoEstacionado(placa.ToString());
                return;
            }
            
            var veiculo = new Veiculo(placa);
            _veiculos.Add(veiculo);

            Historico.VeiculoAdicionado(placa.ToString());
            ConsoleUI.ExibirVeiculoAdicionado(placa.ToString());
        }

        public void RemoverVeiculo()
        {
            Placa placa = ConsoleUI.SolicitarPlacaParaRemover();
            if (!VerificarSeVeiculoExiste(placa)) return;

            int horas = ConsoleUI.SolicitarHorasEstacionadas();
            decimal valorTotal = CalcularValorTotalParaVeiculo(horas);

            _veiculos.Remove(ObterVeiculoPorPlaca(placa));

            ConsoleUI.ExibirVeiculoRemovido(placa.ToString(), horas, valorTotal);
            Historico.VeiculoRemovido(placa.ToString(), horas, valorTotal);
        }

        public void ListarVeiculos()
        {
            ConsoleUI.ListarVeiculos(Veiculos);
            Historico.VeiculosListados(Veiculos);
        }

        public void ExibirHistórico()
        {
            ConsoleUI.ExibirHistórico(Historico);
        }

        private bool VerificarSeVeiculoExiste(Placa placa)
        {
            return _veiculos.Any(v => v.Placa.Equals(placa));
        }

        private Veiculo ObterVeiculoPorPlaca(Placa placa)
        {
            return _veiculos.FirstOrDefault(v => v.Placa.Equals(placa));
        }

        private decimal CalcularValorTotalParaVeiculo(int horas)
        {
            // TODO: Usar padrão strategy e fornecer diferentes cálculos de preço
            return PrecoInicial + (PrecoPorHora * horas);
        }
    }
}
