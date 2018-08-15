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
        private List<TaxEntry> _taxEntries;
        private OrderService _orderService;

        private const string postalCode = "98101";
        private const string country = "USA";

        public OrderServiceTests()
        {
            _customerRepository = MockRepository.GenerateMock<ICustomerRepository>();
            _emailService = MockRepository.GenerateMock<IEmailService>();
            _orderFulfillmentService = MockRepository.GenerateMock<IOrderFulfillmentService>();
            _productRepository = MockRepository.GenerateMock<IProductRepository>();
            _taxRateService = MockRepository.GenerateMock<ITaxRateService>();

            _taxEntries = new List<TaxEntry>()
            {
                CreateTaxEntry("test", 12),
                CreateTaxEntry("test2", 10)
            };

            _productRepository
                .Stub(p => p.IsInStock("product_1"))
                .Return(true);

            _productRepository
                .Stub(p => p.IsInStock("product_2"))
                .Return(true);

            var order = new Order();
            _orderFulfillmentService
                .Stub(o => o.Fulfill(order))
                .Return(new OrderConfirmation()
                {
                    OrderNumber = "1001"
                });

            _taxRateService
                .Stub(t => t.GetTaxEntries(postalCode, country))
                .Return(_taxEntries);

            _orderService = new OrderService(_emailService, _orderFulfillmentService, _productRepository, _taxRateService, postalCode, country);
        }
        public OrderService OrderService { get; set; }

        [Fact]
        public void ReturnsOrderSummary()
        {
            // Given
            var order = new Order();

            // When
            var result = this.OrderService.PlaceOrder(order);

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void SubmitOrderToFulfillment()
        {
            // Given
            var order = new Order();
            _orderFulfillmentService
                .Stub(o => o.Fulfill(order))
                .Return(new OrderConfirmation()
                {
                    OrderNumber = "1001"
                });

            // When
            var result = this.OrderService.PlaceOrder(order);

            // Then
            _orderFulfillmentService.AssertWasCalled(o => o.Fulfill(order));
            Assert.Equal("1001", result.OrderNumber);
        }

        [Fact]
        public void ContainsOrderFulfillmentNumber()
        {
            // Given
            var order = new Order();
            _orderFulfillmentService
                .Stub(o => o.Fulfill(order))
                .Return(new OrderConfirmation()
                {
                    OrderNumber = "1001"
                });

            // When
            var result = this.OrderService.PlaceOrder(order);

            // Then

        }

    }
}

