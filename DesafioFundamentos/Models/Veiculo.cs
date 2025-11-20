namespace DesafioFundamentos.Models
{
	// Representa um veículo com uma Placa
	public class Veiculo : IEquatable<Veiculo>
	{
		public Placa Placa { get; }

		public Veiculo(Placa placa)
		{
			Placa = placa;
		}

		public override string ToString() => Placa?.ToString() ?? string.Empty;

		// IEquatable<Veiculo> - compara pelo valor da Placa
		public bool Equals(Veiculo other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (other is null) return false;

			// Se apenas uma das placas for nula, não são iguais
			if (Placa is null || other.Placa is null) return false;

			return Placa.Equals(other.Placa);
		}

		public override bool Equals(object obj) => Equals(obj as Veiculo);

		public override int GetHashCode() => Placa?.GetHashCode() ?? 0;

		public static bool operator ==(Veiculo left, Veiculo right)
		{
			if (ReferenceEquals(left, right)) return true;
			if (left is null || right is null) return false;
			return left.Equals(right);
		}

		public static bool operator !=(Veiculo left, Veiculo right) => !(left == right);
	}
}
