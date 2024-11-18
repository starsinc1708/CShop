using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.DDDTest
{
	public class ValueObjectTests
	{
		[Fact]
		public void Name_ShouldThrowException_WhenValueIsEmptyOrNull()
		{
			Assert.Throws<ArgumentException>(() => new Name(""));
			Assert.Throws<ArgumentNullException>(() => new Name(null));
		}

		[Fact]
		public void Name_ShouldThrowException_WhenValueExceedsMaxLength()
		{
			var longName = new string('A', 101);
			Assert.Throws<ArgumentException>(() => new Name(longName));
		}

		[Fact]
		public void Price_ShouldThrowException_WhenValueIsNegative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new Price(-1));
		}

		[Fact]
		public void StockDetails_ShouldThrowException_WhenThresholdsAreInvalid()
		{
			Assert.Throws<ArgumentException>(() => new StockDetails(10, 15, 10)); // RestockThreshold > MaxStockThreshold
			Assert.Throws<ArgumentOutOfRangeException>(() => new StockDetails(-5, 2, 10)); // Negative AvailableStock
		}

		[Fact]
		public void StockDetails_ShouldCorrectlySetProperties_WhenValuesAreValid()
		{
			// Arrange
			int availableStock = 10;
			int restockThreshold = 2;
			int maxStockThreshold = 20;

			// Act
			var stockDetails = new StockDetails(availableStock, restockThreshold, maxStockThreshold);

			// Assert
			Assert.Equal(availableStock, stockDetails.AvailableStock);
			Assert.Equal(restockThreshold, stockDetails.RestockThreshold);
			Assert.Equal(maxStockThreshold, stockDetails.MaxStockThreshold);
			Assert.False(stockDetails.MaxStockThreshHoldReached);
		}
	}
}
