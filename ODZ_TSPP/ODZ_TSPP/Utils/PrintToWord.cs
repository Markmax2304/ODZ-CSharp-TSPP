using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace ODZ_TSPP.Utils
{
    public class WordUtils
    {
        private static object missing = Type.Missing;

        #region Public Methods
        public static void PrintToWord(string output)
        {
            PrintToWord(new List<string> { output });
        }

        public static void PrintToWord(IList outputs)
        {
            string path = $"{InvokeFolderBrowser()}\\result.doc";

            Word._Application word_app = new Word.Application();
            Word._Document word_doc = word_app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

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
        }
        #endregion

        #region Private Method
        private static string InvokeFolderBrowser()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.ShowDialog();

            return browser.SelectedPath;
        }
        #endregion
    }
}
