using System;
using System.Collections.Generic;

namespace DesafioFundamentos.Models
{
    public class Historico
    {
        private readonly List<string> _historicoVeiculos = new();

        public IReadOnlyList<string> HistoricoVeiculos => _historicoVeiculos.AsReadOnly();

        public Historico()
        {
            //Construtor explicitamente sem conteúdo
        }

        public void Inserir(string mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem)) return;
            string horario = DateTime.Now.ToString("g");
            _historicoVeiculos.Add($"{horario} - {mensagem}");
        }

        public void VeiculoAdicionado(string placa)
        {
            Inserir($"Veículo com placa {placa} adicionado");
        }

        public void VeiculoRemovido(string placa, int horas, decimal valorTotal)
        {
            Inserir($"Veículo com placa {placa} removido após {horas} horas. Valor total: R$ {valorTotal:0.00}");
        }

        public void VeiculosListados(IEnumerable<Veiculo> veiculos)
        {
            Inserir($"Listagem de veículos realizada: {veiculos.Count()} veículos estacionados");
        }

    }
}