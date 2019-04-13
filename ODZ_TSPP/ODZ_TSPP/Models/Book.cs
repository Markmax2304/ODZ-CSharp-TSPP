using System;
using System.Text;

namespace ODZ_TSPP
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Interval Limit { get; set; }

        public Book(int id, string title, double price, int quantity, Interval limit)
        {
            Id = id;
            Title = title;
            Price = price;
            Quantity = quantity;
            Limit = limit;
        }

        public override string ToString()
        {
            StringBuilder value = new StringBuilder();
            value.AppendFormat($"Id = {Id} | Title = {Title} | Price = {Price} | Quantity = {Quantity} |" +
                $" Age limit: from {Limit.from} till {Limit.till}.");
            return value.ToString();
        }

        public string ToData()
        {
            throw new NotImplementedException();
        }

        public Book ToObject()
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
