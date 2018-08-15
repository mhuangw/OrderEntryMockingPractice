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
        private Order _order;

        private const string product_sku_1 = "product_sku_1";
        private const string product_sku_2 = "product_sku_2";
        private const string product_sku_3 = "product_sku_3";

        private const int customer_id_1 = 12345;
        private const int customer_id_2 = 23456;

        private const int order_id_1 = 12;
        private const int order_id_2 = 14;

        private const string order_no_1 = "1123";
        private const string order_no_2 = "2332";

        private const int quantity = 2;

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
        private static OrderItem CreateOrderItemObject(string product_sku, int quantity, decimal price)
        {
            var orderItem = new OrderItem
            {
                Product = new Product(),
                Quantity = quantity
            };
            orderItem.Product.Sku = product_sku;
            orderItem.Product.Price = price;
            return orderItem;
        }

        private Order CreateOrderObject(string product_sku_1, string product_sku_2, int customer_id = customer_id_1)
        {
            _order = new Order();
            OrderItem orderItem1 = CreateOrderItemObject(product_sku_1, 1, 30);
            OrderItem orderItem2 = CreateOrderItemObject(product_sku_2, 2, 10);

            _order.OrderItems = new List<OrderItem>
            {
                orderItem1,
                orderItem2
            };
            _order.CustomerId = customer_id;
            return _order;
        }

        private OrderConfirmation CreateOrderConfirmationObject(int customerId, int orderId, string orderNumber)
        {
            var orderConfirmationObject = new OrderConfirmation();
            orderConfirmationObject.CustomerId = customerId;
            orderConfirmationObject.OrderId = orderId;
            orderConfirmationObject.OrderNumber = orderNumber;
            return orderConfirmationObject;
        }

        private TaxEntry CreateTaxEntry(string tax_description, decimal tax_rate)
        {
            TaxEntry taxEntry = new TaxEntry
            {
                Description = tax_description,
                Rate = tax_rate
            };
            return taxEntry;
        }

    }
}

