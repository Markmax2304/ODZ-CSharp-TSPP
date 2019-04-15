using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODZ_TSPP.Utils;

namespace ODZ_TSPP.Commands
{
    public class FindCheapestCommand : ICommandButton
    {
        private Model _context;
        private View _view;

        public FindCheapestCommand(Model context, View view)
        {
            _context = context;
            _view = view;
        }

        public void Execute()
        {
            List<Book> books = _context.GetAllBooks();

            Book cheapestBook = null;
            foreach (Book book in books) {
                if(cheapestBook == null || book.Price < cheapestBook.Price) {
                    cheapestBook = book;
                }
            }

            if (cheapestBook != null) {
                string value = $"The cheapest book is \"{cheapestBook.Title}\" and its price = {cheapestBook.Price}";
                _view.SetOutputField(value);
                string filename = _view.GetFileNameField();
                WordUtils.PrintToWord(value, filename);
            }
        }
    }
}
