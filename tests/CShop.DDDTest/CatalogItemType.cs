using System.Xml.Linq;
using Xunit;

namespace CShop.DDDTest
{
	public class CatalogItemTypeTests
	{
		[Fact]
		public void Constructor_ShouldInitializeCatalogItemType()
		{
			// Arrange
			var name = new Name("Type A");

			// Act
			var itemType = new CatalogItemType(name);

			// Assert
			Assert.Equal(name, itemType.Name);
			Assert.True(itemType.IsActive);
		}

		[Fact]
		public void Deactivate_ShouldSetIsActiveToFalse_WhenCalled()
		{
			// Arrange
			var itemType = new CatalogItemType(new Name("Type A"));

			// Act
			itemType.Deactivate();

			// Assert
			Assert.False(itemType.IsActive);
		}

		[Fact]
		public void Activate_ShouldThrowException_WhenItemTypeIsAlreadyActive()
		{
			// Arrange
			var itemType = new CatalogItemType(new Name("Type A"));

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => itemType.Activate());
		}
	}
}
