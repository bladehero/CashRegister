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

        public OrderSM(SessionSM session)
        {
            Session = session;
            _products = Array.Empty<OrderProductSM>();
        }
        public OrderSM(SessionSM session, IEnumerable<OrderProductSM> products)
        {
            Session = session;
            _products = products;
        }

        public int Id { get; set; }
        public SessionSM Session { get; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public int Count => _count ??= _products.Count();
        public decimal Sum => _products.Sum(x => x.TotalPrice);

        public IEnumerator<OrderProductSM> GetEnumerator() => _products.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _products.GetEnumerator();
    }
}