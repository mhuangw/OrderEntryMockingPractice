using System;
using System.Runtime.Serialization;

namespace OrderEntryMockingPractice.Services
{
    [Serializable]
    public class OrderItemsAreNotUniqueException : Exception
    {
        public OrderItemsAreNotUniqueException()
        {
        }

        public OrderItemsAreNotUniqueException(string message) : base(message)
        {
        }

        public OrderItemsAreNotUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderItemsAreNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}