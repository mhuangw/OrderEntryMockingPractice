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
            var stubProduct = new Product();

            // Act
            bool isInStock = stubProductProvider.IsInStock(stubProduct.Sku);

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
            var stubCustomerProvider = MockRepository.GenerateStub<ICustomerRepository>();
            var stubCustomer = MockRepository.GenerateStub<Customer>();

            // Act
            var customer = stubCustomerProvider.Get(stubCustomer.CustomerId.Value);

            // Assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void TaxesCanBeRetrieved()
        {
            // Arrange
            var stubTaxesProvider = MockRepository.GenerateStub<ITaxRateService>();
            var stubCustomer = MockRepository.GenerateStub<Customer>();

            // Act
            var stubTaxes = stubTaxesProvider.GetTaxEntries(stubCustomer.PostalCode, stubCustomer.Country);

            // Assert
            Assert.NotEmpty(stubTaxes);
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
