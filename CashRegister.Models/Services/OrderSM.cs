using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister.Models.Services
{
    public class OrderSM : IReadOnlyCollection<OrderProductSM>
    {
        private readonly IEnumerable<OrderProductSM> _products;
        private int? _count = null;

        public OrderSM()
        {
            _products = Array.Empty<OrderProductSM>();
        }
        public OrderSM(IEnumerable<OrderProductSM> products)
        {
            _products = products;
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int Count => _count ??= _products.Count();
        public decimal Sum => _products.Sum(x => x.Quantity * x.Product.Price);

        public IEnumerator<OrderProductSM> GetEnumerator() => _products.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _products.GetEnumerator();
    }
}