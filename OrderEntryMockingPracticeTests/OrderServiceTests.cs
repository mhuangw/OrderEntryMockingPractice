using Xunit;
using Rhino.Mocks;
using OrderEntryMockingPractice.Services;
using OrderEntryMockingPractice.Models;

namespace OrderEntryMockingPracticeTests
{
    public class OrderServiceTests
    {
        [Fact]
        public void OrderItemsAreUnique()
        {
            // Arrange
            //var stubOrderProvider = MockRepository.GenerateStub<>

            // Act

            // Assert

        }

        [Fact]
        public void AllProductsAreInStock()
        {
            // Arrange
            var stubProductProvider = MockRepository.GenerateStub<IProductRepository>();
            var product = new Product();

            // Act
            bool isInStock = stubProductProvider.IsInStock(product.Sku);

            // Assert
            Assert.True(isInStock);
        }

        [Fact]
        public void OrderIsValid()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CustomerInfoCanBeRetrieved()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void TaxesCanBeRetrieved()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void ProductRepositoryCanDetermineStock()
        {
            // Arrange

            // Act

            // Assert
        }
    
    }
}
