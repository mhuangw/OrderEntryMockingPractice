using OrderEntryMockingPractice.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderEntryMockingPractice.Services
{
    public class OrderService
    {
        private IEmailService _emailService;
        private IOrderFulfillmentService _orderFulfillmentService;
        private IProductRepository _productRepository;
        private ITaxRateService _taxRateService;
        private string _postalCode;
        private string _country;

        public OrderService(IProductRepository productRepository,
            ITaxRateService taxRateService,
            IEmailService emailService,
            IOrderFulfillmentService orderFulfillmentService,
            string postalCode, string country)
        {
            _productRepository = productRepository;
            _taxRateService = taxRateService;
            _orderFulfillmentService = orderFulfillmentService;
            _emailService = emailService;
            _postalCode = postalCode;
            _country = country;
        }

        public bool ProductsAreInStock(Order order, IProductRepository productRepository)
        {
            foreach (var orderItem in order.OrderItems.ToList())
                if (productRepository.IsInStock(orderItem.Product.Sku) == false)
                    return false;
            return true;
        }

        public decimal GetTotalTax(IEnumerable<TaxEntry> taxEntries)
        {
            decimal total_tax = 0;

            foreach (TaxEntry entry in taxEntries)
                total_tax += entry.Rate;

            return total_tax;
        }

        public OrderService(IEmailService emailService, 
                            IOrderFulfillmentService orderFulfillmentService,
                            IProductRepository productRepository,
                            ITaxRateService taxRateService,
                            string postalCode, string country)
        {
            _productRepository = productRepository;
            _orderFulfillmentService = orderFulfillmentService;
            _productRepository = productRepository;
            _taxRateService = taxRateService;
            _postalCode = postalCode;
            _country = country;
        }

        public OrderSummary PlaceOrder(Order order)
        {
            if (order.OrderItemsAreUnique() == false)
            {
                throw new OrderItemsAreNotUniqueException();
            }

            if (ProductsAreInStock(order, _productRepository) == false)
            {
                throw new OrderItemsAreNotInStockException();
            }

            OrderConfirmation orderConfirmation = _orderFulfillmentService.Fulfill(order);
            IEnumerable<TaxEntry> taxEntries = _taxRateService.GetTaxEntries(_postalCode, _country);

            decimal netTotal = order.GetOrderTotal();
            decimal orderTotal = netTotal + GetTotalTax(taxEntries);

            OrderSummary orderSummary = new OrderSummary
            {
                OrderId = orderConfirmation.OrderId,
                OrderNumber = orderConfirmation.OrderNumber,
                CustomerId = orderConfirmation.CustomerId,  
                OrderItems = order.OrderItems,
                NetTotal = netTotal,
                Taxes = taxEntries,
                Total = orderTotal
            };

            _emailService.SendOrderConfirmationEmail(orderConfirmation.CustomerId, orderConfirmation.OrderId);

            return orderSummary;
        }
    }
}
