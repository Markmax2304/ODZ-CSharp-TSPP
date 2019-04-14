using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODZ_TSPP.Commands
{
    class FindByConfines : ICommandButton
    {
        private Model _context;
        private View _view;

        public FindByConfines(Model context, View view)
        {
            _context = context;
            _view = view;
        }

        public void Execute()
        {
            Interval confines = _view.GetLimitFields();
            List<Book> books = _context.GetAllBooks();

            Book cheapestBook = null;
            foreach(Book book in books) {
                if(book.Limit.from >= confines.from && book.Limit.till <= confines.till) {
                    _view.SetOutputField(book.Title);

                    if(cheapestBook == null || cheapestBook.Price > book.Price) {
                        cheapestBook = book;
                    }
                }
            }

            if(cheapestBook != null) {
                _view.SetOutputField($"The cheapest book is \"{cheapestBook.Title}\" and its price = {cheapestBook.Price}");
            }
        }
    }
}
