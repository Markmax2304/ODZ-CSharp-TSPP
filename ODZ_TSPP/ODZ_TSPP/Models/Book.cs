using System;
using System.Text;
using ODZ_TSPP.Models;

namespace ODZ_TSPP
{
    public class Book : IDatable
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Interval Limit { get; set; }

        public Book(string title, double price, int quantity, Interval limit)
        {
            Title = title;
            Price = price;
            Quantity = quantity;
            Limit = limit;
        }

        public string ToString()
        {
            StringBuilder value = new StringBuilder();
            value.AppendFormat("Title = {0} | Price = {1} | Quantity = {2} | Age limit: from {3} till {4}.", Title, Price, Quantity, Limit.from, Limit.till);
            return value.ToString();
        }

        public string ToData()
        {
            throw new NotImplementedException();
        }

        public IDatable ToObject()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public struct Interval
    {
        public int from;
        public int till;

        public Interval(int _from, int _till)
        {
            from = _from;
            till = _till;
        }
    }
}
