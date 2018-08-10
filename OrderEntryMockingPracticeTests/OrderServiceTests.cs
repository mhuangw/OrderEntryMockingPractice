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
            HashSet<string> skusSeen = new HashSet<string>();

            foreach (OrderItem orderItem in order.OrderItems)
            {
                if (skusSeen.Contains(orderItem.Product.Sku))
                {
                    return false;
                }
                skusSeen.Add(orderItem.Product.Sku);
            }
            return true;
        }

        [Fact]
        public void OrderItemsAreUnique()
        {
            // Arrange
            var orderStub = MockRepository.GenerateStub<Order>();

            // Act
            bool orderItemsAreUnique = IsUnique(orderStub);

            // Assert
            Assert.True(orderItemsAreUnique);
        }

        [Fact]
        public void AllProductsAreInStock()
        {
            // Arrange
            var productRepositoryStub = MockRepository.GenerateStub<IProductRepository>();
            var productStub = MockRepository.GenerateStub<Product>();

            // Act
            bool isInStock = productRepositoryStub.IsInStock(productStub.Sku);

            // Assert
            Assert.True(isInStock);
        }

        public void OrderIsValid()
        {
            bool isValid = true;
            if (isValid)
            {
                Order orderStub = MockRepository.GenerateStub<Order>();
                _orderFulfillmentService.Fulfill(orderStub);
                _emailService.SendOrderConfirmationEmail(customerId, orderId);
            } else
            {
                throw new Exception();
            }
        }

        [Fact]
        public void CustomerInfoCanBeRetrieved()
        {
            // Arrange
            var customerRepositoryStub = MockRepository.GenerateStub<ICustomerRepository>();
            var customerStub = MockRepository.GenerateStub<Customer>();

            // Act
            var customer = customerRepositoryStub.Get(customerStub.CustomerId.Value);

            // Assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void TaxesCanBeRetrieved()
        {
            // Arrange
            var taxRateServiceStub = MockRepository.GenerateStub<ITaxRateService>();
            var customerStub = MockRepository.GenerateStub<Customer>();

            // Act
            var taxesStub = taxRateServiceStub.GetTaxEntries(customerStub.PostalCode, customerStub.Country);

            // Assert
            Assert.NotEmpty(taxesStub);
        }

        [Fact]
        public void ProductRepositoryCanDetermineStock()
        {
            // Arrange
            var productRepositoryStub = MockRepository.GenerateStub<IProductRepository>();
            var productStub = MockRepository.GenerateStub<Product>();

            // Act
            var isInStock = productRepositoryStub.IsInStock(productStub.Sku);

            // Assert
            Assert.True(isInStock);
        }
    
    }
}
