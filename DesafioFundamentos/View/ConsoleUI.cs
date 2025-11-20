using System.Text;
using DesafioFundamentos.Models;

namespace DesafioFundamentos.View
{
    public static class ConsoleUI
    {
        //Todo: usar arquivos de recursos (.resx) para mapear as strings
        /*
        Para criar um "mapa de strings" no C# que seja compreendido pelo 
        código-fonte de forma eficiente e com boas práticas, a abordagem 
        recomendada é utilizar arquivos de recursos (.resx). Isso oferece 
        acesso fortemente tipado (strong-typed access) às strings, o que 
        permite ao compilador do C# verificar a existência e o tipo das 
        strings em tempo de compilação, além de facilitar a localização 
        (suporte a múltiplos idiomas). 
        */
   
        public static void ExibirMenu(Estacionamento estacionamento)
		{
			string opcao = string.Empty;
			bool exibirMenu = true;

			while (exibirMenu)
			{
				Console.Clear();
				Console.WriteLine("**************************************");
				Console.WriteLine("Estacionamento 'ESTACIONA FÁCIL'");
				Console.WriteLine($"  - Preço inicial: R$ {estacionamento.PrecoInicial:0.00}");
				Console.WriteLine($"  - Preço por hora: R$ {estacionamento.PrecoPorHora:0.00}");
				Console.WriteLine($"  - Veículos estacionados: {estacionamento.Veiculos.Count}");
				Console.WriteLine("**************************************\n");
				Console.WriteLine("Digite a sua opção:");
				Console.WriteLine("1 - Cadastrar veículo");
				Console.WriteLine("2 - Remover veículo");
				Console.WriteLine("3 - Listar veículos");
				Console.WriteLine("4 - Histórico");
				Console.WriteLine("5 - Encerrar");

				Console.Write("\nOpção: ");
				opcao = Console.ReadLine() ?? string.Empty;

				switch (opcao)
				{
					case "1":
						estacionamento.AdicionarVeiculo();
						break;

					case "2":
						estacionamento.RemoverVeiculo();
						break;

					case "3":
						estacionamento.ListarVeiculos();
						break;

					case "4":
						estacionamento.ExibirHistórico();
						break;

					case "5":
						exibirMenu = false;
						break;

					default:
						Console.WriteLine("Opção inválida");
						break;
				}

				Console.WriteLine("\nPressione uma tecla para continuar\n");
				Console.ReadLine();
			}
		}

        public static void ExibirMensagem(string mensagem)
        {
            Console.WriteLine($"\n{mensagem}");
        }

        public static void ExibirMensagemDeErroAoRemover()
        {
            ExibirMensagem("Desculpe, esse veículo não está estacionado aqui. " +
            "Confira se digitou a placa corretamente!");
        }

        public static void ExibirMensagemFinal()
        {
            ExibirMensagem("O programa se encerrou");
        }

        public static void ExibirMensagemInicial()
        {
            Console.Clear();
    
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("********************************************");
            sb.AppendLine("*     Estacionamento 'ESTACIONA FÁCIL'     *");
            sb.AppendLine("********************************************\n");
            sb.AppendLine("Seja bem vindo ao sistema de estacionamento!");
            sb.AppendLine("Vamos configurar o sistema para este dia.\n");

            Console.WriteLine(sb.ToString());
        }

        public static void ExibirVeiculoAdicionado(string placa)
        {
            ExibirMensagem($"Veículo {placa} adicionado com sucesso!");
        }

        public static void ExibirVeiculoEstacionado(string placa)
        {
            ExibirMensagem($"Veículo com placa {placa} já está estacionado.");
        }

        public static void ExibirVeiculoRemovido(string placa, int horas, decimal valorTotal)
        {
            string mensagemRemocao = $"O veículo {placa} foi removido após {horas} horas" +
            $" e o preço total foi de: R$ {valorTotal:0.00}";
            
            ConsoleUI.ExibirMensagem(mensagemRemocao);
        }

        public static decimal LerDecimal(string prompt)
        {
            decimal valor;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out valor) || valor < 0)
            {
                Console.WriteLine("Entrada inválida. Por favor, digite um número válido.");
                Console.Write(prompt);
            }
            return valor;
        }

        public static void ListarVeiculos(IReadOnlyList<Veiculo> veiculos)
        {
            // Verifica se há veículos no estacionamento
            if (!veiculos.Any())
            {
                ExibirMensagem("Não há veículos estacionados.");
                return;
            }

            Console.WriteLine("\nOs veículos estacionados são:");
            for (int i = 0; i < veiculos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {veiculos[i].Placa}");
            }
        }

        public static int SolicitarHorasEstacionadas()
        {
            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
            Console.Write("Horas: ");

            int horas = 0;

            while (true)
            {
                string entrada = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(entrada, out horas) && horas >= 0) break;

                Console.WriteLine("Entrada inválida. Por favor, digite um número válido de horas.");
            }

            return horas;
        }

        public static Placa SolicitarPlacaParaEstacionar()
        {
            string mensagem = "Digite a placa do veículo para estacionar:";
            return SolicitarPlacaValidaProUsuario(mensagem);
        }

        public static Placa SolicitarPlacaParaRemover()
        {
            string mensagem = "Digite a placa do veículo para remover:";
            return SolicitarPlacaValidaProUsuario(mensagem);
        }

        // Agora retorna um objeto Placa válido
        public static Placa SolicitarPlacaValidaProUsuario(string mensagem)
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                Console.Write("Placa: ");
                string entrada = Console.ReadLine() ?? string.Empty;
                string placaTexto = entrada.Trim().ToUpperInvariant();

                if (Placa.ValidarTextoPlaca(placaTexto))
                    return new Placa(placaTexto);

                Console.WriteLine("Placa inválida. Tente novamente.");
            }
        }

        public static void ExibirHistórico(Historico historico)
        {
            if (!historico.HistoricoVeiculos.Any())
            {
                Console.WriteLine("\nNão há histórico para mostrar.");
                return;
            }

            Console.WriteLine("\nHistórico do estacionamento:");

            for (int i = 0; i < historico.HistoricoVeiculos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {historico.HistoricoVeiculos[i]}");
            }
        }
    }
}