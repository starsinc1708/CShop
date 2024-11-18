using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot.ValueObjects
{
	public class Name : ValueObject
	{
		public string Value { get; private set; }

		public readonly int _maxLength = 100;

		public Name(string value) 
		{
			ArgumentException.ThrowIfNullOrEmpty(value, nameof(Name));
			ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));

			if (value.Length > _maxLength)
				throw new ArgumentException($"{nameof(Name)} cannot exceed {_maxLength} characters");

			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
