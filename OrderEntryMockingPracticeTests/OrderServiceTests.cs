using Xunit;
using Rhino.Mocks;
using OrderEntryMockingPractice.Services;
using OrderEntryMockingPractice.Models;
using System.Collections.Generic;

namespace OrderEntryMockingPracticeTests
{
    public class CMOrderServiceTests
    {
        private ICustomerRepository _customerRepository;
        private IEmailService _emailService;
        private IOrderFulfillmentService _orderFulfillmentService;
        private IProductRepository _productRepository;
        private ITaxRateService _taxRateService;

        public CMOrderServiceTests()
        {
            _customerRepository = MockRepository.GenerateMock<ICustomerRepository>();
            _emailService = MockRepository.GenerateMock<IEmailService>();
            _orderFulfillmentService = MockRepository.GenerateMock<IOrderFulfillmentService>();
            _productRepository = MockRepository.GenerateMock<IProductRepository>();
            _taxRateService = MockRepository.GenerateMock<ITaxRateService>();

            this.OrderService = new OrderService();
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

