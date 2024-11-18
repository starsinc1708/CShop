using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot.ValueObjects
{
	public class Price : ValueObject
	{
		public decimal Value { get; }

		public Price(decimal value)
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException(nameof(Price), "Price cannot be negative.");

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
