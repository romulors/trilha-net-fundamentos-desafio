using DesafioFundamentos.Models;
using DesafioFundamentos.View;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

ConsoleUI.ExibirMensagemInicial();

decimal precoInicial = ConsoleUI.LerDecimal("Digite o preço inicial (em R$): ");
decimal precoPorHora = ConsoleUI.LerDecimal("Digite o preço por hora (em R$): ");
Estacionamento estacionamento = new(precoInicial, precoPorHora);

ConsoleUI.ExibirMenu(estacionamento);

ConsoleUI.ExibirMensagemFinal();