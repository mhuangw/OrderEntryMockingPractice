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
            var stubOrderProvider = MockRepository.GenerateStub<>

            // Act

            // Assert

        }

        [Fact]
        public void AllProductsAreInStock()
        {
            // Arrange
            var stubProductRepository = MockRepository.GenerateStub<IProductRepository>();
            var stubProduct = MockRepository.GenerateStub<Product>();

            // Act
            bool isInStock = stubProductRepository.IsInStock(stubProduct.Sku);

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
            var stubCustomerRepository = MockRepository.GenerateStub<ICustomerRepository>();
            var stubCustomer = MockRepository.GenerateStub<Customer>();

            // Act
            var customer = stubCustomerRepository.Get(stubCustomer.CustomerId.Value);

            // Assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void TaxesCanBeRetrieved()
        {
            // Arrange
            var stubTaxRateService = MockRepository.GenerateStub<ITaxRateService>();
            var stubCustomer = MockRepository.GenerateStub<Customer>();

            // Act
            var stubTaxes = stubTaxRateService.GetTaxEntries(stubCustomer.PostalCode, stubCustomer.Country);

            // Assert
            Assert.NotEmpty(stubTaxes);
        }

        [Fact]
        public void ProductRepositoryCanDetermineStock()
        {
            // Arrange
            var stubProductRepository = MockRepository.GenerateStub<IProductRepository>();
            var stubProduct = MockRepository.GenerateStub<Product>();

            // Act
            var IsInStock = stubProductRepository.IsInStock(stubProduct.Sku);

            // Assert
            Assert.IsType<bool>(IsInStock);
        }
    
    }
}
