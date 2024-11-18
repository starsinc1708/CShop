using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot.ValueObjects
{
	public class Description : ValueObject
	{
		public string Value { get; }

		private readonly int _maxLength = 250;

		public Description(string value)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(value);

			if (value.Length > _maxLength)
				throw new ArgumentException($"{nameof(Description)} cannot exceed {_maxLength} characters.", nameof(Description));

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
