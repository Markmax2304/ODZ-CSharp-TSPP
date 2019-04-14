using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace ODZ_TSPP.Commands
{
    class PrintToWord : ICommandButton
    {
        private Model _context;
        private View _view;

        private object missing = Type.Missing;

        public PrintToWord(Model context, View view)
        {
            _context = context;
            _view = view;
        }

        public void Execute()
        {
            string path = $"{InvokeFolderBrowser()}\\result.doc";

            Word._Application word_app = new Word.Application();
            Word._Document word_doc = word_app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            IList outputs = _view.GetOutputField();
            foreach(var row in outputs) 
            {
                Word.Paragraph para = word_doc.Paragraphs.Add(ref missing);
                para.Range.Text = row.ToString();
                para.Range.InsertParagraphAfter();
            }

            object filepath = path;
            word_doc.SaveAs(ref filepath, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);

            object save_changes = false;
            word_doc.Close(ref save_changes, ref missing, ref missing);
            word_app.Quit(ref save_changes, ref missing, ref missing);

            _view.SetOutputField($"Output window is print to word succesfully");
        }

        #region Private Method
        private string InvokeFolderBrowser()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.ShowDialog();

            return browser.SelectedPath;
        }
        #endregion
    }
}
