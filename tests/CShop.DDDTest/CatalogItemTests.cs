using System.Xml.Linq;

namespace CShop.DDDTest
{
	public class CatalogItemTests
	{
		[Fact]
		public void Constructor_ShouldInitializeCatalogItem_WhenValidParametersAreProvided()
		{
			// Arrange
			var name = new Name("Test Item");
			var description = new ItemDescription("Test Description");
			var price = new Price(100);
			var stock = new StockDetails(10, 2, 20);

			// Act
			var item = new CatalogItem(name, description, price, stock);

			// Assert
			Assert.Equal(name, item.Name);
			Assert.Equal(description, item.Description);
			Assert.Equal(price, item.Price);
			Assert.Equal(stock, item.Stock);
			Assert.True(item.IsActive);
		}

		[Fact]
		public void ReduceStock_ShouldReduceAvailableStock_WhenQuantityIsValid()
		{
			// Arrange
			var stock = new StockDetails(10, 2, 20);
			var item = CreateTestCatalogItem(stock);

			// Act
			item.ReduceStock(5);

			// Assert
			Assert.Equal(5, item.Stock.AvailableStock);
		}

		[Fact]
		public void ReduceStock_ShouldThrowException_WhenQuantityExceedsAvailableStock()
		{
			// Arrange
			var stock = new StockDetails(10, 2, 20);
			var item = CreateTestCatalogItem(stock);

			// Act & Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => item.ReduceStock(15));
		}

		[Fact]
		public void IncreaseStock_ShouldIncreaseAvailableStock_WhenQuantityIsValid()
		{
			// Arrange
			var stock = new StockDetails(10, 2, 20);
			var item = CreateTestCatalogItem(stock);

			// Act
			item.IncreaseStock(5);

			// Assert
			Assert.Equal(15, item.Stock.AvailableStock);
		}

		[Fact]
		public void Deactivate_ShouldSetIsActiveToFalse_WhenCalled()
		{
			// Arrange
			var item = CreateTestCatalogItem();

			// Act
			item.Deactivate();

			// Assert
			Assert.False(item.IsActive);
		}

		[Fact]
		public void Activate_ShouldSetIsActiveToTrue_WhenCalled()
		{
			// Arrange
			var item = CreateTestCatalogItem();
			item.Deactivate();

			// Act
			item.Activate();

			// Assert
			Assert.True(item.IsActive);
		}

		private CatalogItem CreateTestCatalogItem(StockDetails? stock = null)
		{
			var name = new Name("Test Item");
			var description = new ItemDescription("Test Description");
			var price = new Price(100);
			var stockDetails = stock ?? new StockDetails(10, 2, 20);
			return new CatalogItem(name, description, price, stockDetails);
		}
	}
}
