using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
	// Wrapper simples para uma placa (string) com validação
	public sealed class Placa : IEquatable<Placa>
	{
		private static readonly Regex PadraoMercosul = new(@"^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex PadraoBrasil = new(@"^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		public string Valor { get; }

		public Placa(string valor)
		{
            if (!ValidarTextoPlaca(valor)) throw new ArgumentException($"Placa {valor} inválida.", nameof(valor));

			Valor = valor;
		}

		public static bool ValidarTextoPlaca(string valor)
		{
			if (string.IsNullOrWhiteSpace(valor)) return false;
			valor = valor.Trim().ToUpperInvariant();
			return PadraoMercosul.IsMatch(valor) || PadraoBrasil.IsMatch(valor);
		}

		public bool Equals(Placa outraPlaca)
		{
			if (ReferenceEquals(this, outraPlaca)) return true;
			if (outraPlaca is null) return false;
			return string.Equals(Valor, outraPlaca.Valor, StringComparison.Ordinal);
		}

		public override bool Equals(object obj) => obj is Placa outraPlaca && Equals(outraPlaca);

		public override int GetHashCode() => Valor?.GetHashCode() ?? 0;

        public override string ToString() => Valor;
	}
}
