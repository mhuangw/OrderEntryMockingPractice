using Xunit;
using Rhino.Mocks;
using OrderEntryMockingPractice.Services;
using OrderEntryMockingPractice.Models;
using System.Collections.Generic;

namespace OrderEntryMockingPracticeTests
{
    public class OrderServiceTests
    {
        private ICustomerRepository _customerRepository;
        private IEmailService _emailService;
        private IOrderFulfillmentService _orderFulfillmentService;
        private IProductRepository _productRepository;
        private ITaxRateService _taxRateService;

        public OrderServiceTests()
        {
            _customerRepository = MockRepository.GenerateMock<ICustomerRepository>();
            _emailService = MockRepository.GenerateMock<IEmailService>();
            _orderFulfillmentService = MockRepository.GenerateMock<IOrderFulfillmentService>();
            _productRepository = MockRepository.GenerateMock<IProductRepository>();
            _taxRateService = MockRepository.GenerateMock<ITaxRateService>();
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
            var order = MockRepository.GenerateStub<Order>();

            // Act
            bool orderItemsAreUnique = IsUnique(order);

            // Assert
            Assert.True(orderItemsAreUnique);
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

        public OrderSummary OrderIsValid()
        {
            
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
