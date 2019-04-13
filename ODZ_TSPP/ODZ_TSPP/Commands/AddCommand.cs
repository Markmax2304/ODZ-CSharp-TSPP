using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODZ_TSPP.Commands
{
    public class AddCommand : ICommandButton
    {
        private Model _context;
        private View _view;

        public AddCommand(Model context, View view)
        {
            _context = context;
            _view = view;
        }

        public void Execute()
        {
            string title = _view.GetTitleField();
            double price = _view.GetPriceField();
            int quantity = _view.GetQuantityField();
            Interval confines = _view.GetLimitFields();

            Book book = new Book(1000, title, price, quantity, confines);

            _context.AddBook(book);
        }
    }
}
