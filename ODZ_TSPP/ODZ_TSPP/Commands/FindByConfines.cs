using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODZ_TSPP.Utils;

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
            List<string> outputs = new List<string>();

            foreach(Book book in books) {
                if(book.Limit.from >= confines.from && book.Limit.till <= confines.till) {
                    string value = $"\"{book.Title}\" : {book.Limit.ToString()}";
                    _view.SetOutputField(value);
                    outputs.Add(value);
                }
            }

            WordUtils.PrintToWord(outputs);
        }
    }
}
