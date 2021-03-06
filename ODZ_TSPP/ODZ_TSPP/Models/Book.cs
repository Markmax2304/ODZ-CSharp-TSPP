﻿using System;
using System.Text;

namespace ODZ_TSPP
{
    public class Book
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

        public string ToPrint()
        {
            StringBuilder value = new StringBuilder();
            value.AppendFormat($"Title = {Title} | Price = {Price} | Quantity = {Quantity} |" +
                $" Age limit: from {Limit.from} till {Limit.till}.");
            return value.ToString();
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

        public override string ToString()
        {
            return $"{from}-{till}";
        }
    }
}
