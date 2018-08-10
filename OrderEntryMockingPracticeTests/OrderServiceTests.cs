using Xunit;
using Rhino.Mocks;
using OrderEntryMockingPractice.Services;
using OrderEntryMockingPractice.Models;
using System.Collections.Generic;

namespace OrderEntryMockingPracticeTests
{
    public class OrderServiceTests
    {

        public OrderServiceTests()
        {

        }

        public bool IsUnique(Order order)
        {
            HashSet<string> SkusSeen = new HashSet<string>();

            foreach (OrderItem orderItem in order.OrderItems)
            {
                if (SkusSeen.Contains(orderItem.Product.Sku))
                {
                    return false;
                }
                SkusSeen.Add(orderItem.Product.Sku);
            }
            return true;
        }

        [Fact]
        public void OrderItemsAreUnique()
        {
            // Arrange
            var OrderStub = MockRepository.GenerateStub<Order>();

            // Act
            bool orderItemsAreUnique = IsUnique(OrderStub);

            // Assert
            Assert.True(orderItemsAreUnique);
        }

        [Fact]
        public void AllProductsAreInStock()
        {
            // Arrange
            var ProductRepositoryStub = MockRepository.GenerateStub<IProductRepository>();
            var ProductStub = MockRepository.GenerateStub<Product>();

            // Act
            bool isInStock = ProductRepositoryStub.IsInStock(ProductStub.Sku);

            // Assert
            Assert.True(isInStock);
        }

        public OrderSummary OrderIsValid()
        {
            
        }

        [Fact]
        public void CustomerInfoCanBeRetrieved()
        {
            // Arrange
            var CustomerRepositoryStub = MockRepository.GenerateStub<ICustomerRepository>();
            var CustomerStub = MockRepository.GenerateStub<Customer>();

            // Act
            var customer = CustomerRepositoryStub.Get(CustomerStub.CustomerId.Value);

            // Assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void TaxesCanBeRetrieved()
        {
            // Arrange
            var TaxRateServiceStub = MockRepository.GenerateStub<ITaxRateService>();
            var CustomerStub = MockRepository.GenerateStub<Customer>();

            // Act
            var TaxesStub = TaxRateServiceStub.GetTaxEntries(CustomerStub.PostalCode, CustomerStub.Country);

            // Assert
            Assert.NotEmpty(TaxesStub);
        }

        [Fact]
        public void ProductRepositoryCanDetermineStock()
        {
            // Arrange
            var ProductRepositoryStub = MockRepository.GenerateStub<IProductRepository>();
            var ProductStub = MockRepository.GenerateStub<Product>();

            // Act
            var isInStock = ProductRepositoryStub.IsInStock(ProductStub.Sku);

            // Assert
            Assert.IsType<bool>(isInStock);
        }
    
    }
}
