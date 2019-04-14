using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ODZ_TSPP.Commands
{
    public class ShowCommand : ICommandButton
    {
        private Model _context;
        private View _view;

        public ShowCommand(Model context, View view)
        {
            _context = context;
            _view = view;
        }

        public void Execute()
        {
            List<Book> books = _context.GetAllBooks();
            foreach(Book book in books) {
                _view.SetOutputField(book.ToPrint());
            }
        }
    }
}
