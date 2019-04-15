using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ODZ_TSPP.Commands;

namespace ODZ_TSPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        private ViewModel viewModel;

        public bool IsUpdate { get; private set; }

        public View()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            viewModel = new ViewModel(this);
        }

        #region Public Methods
        public void SetOutputField(string value)
        {
            outputText.Items.Add(value);
        }

        public ItemCollection GetOutputField()
        {
            return outputText.Items;
        }

        public void ClearOutputField()
        {
            outputText.Items.Clear();
        }

        public string GetFileNameField()
        {
            string fileName = path.Text;
            if (String.IsNullOrEmpty(fileName)) {
                return "";
            }
            return fileName;
        }

        public string GetTitleField()
        {
            string name = nameText.Text;
            if (String.IsNullOrEmpty(name)) {
                throw new Exception("Name field is empty. Change this and try again.");
            }
            return name;
        }

        public double GetPriceField()
        {
            string price = priceText.Text;
            if (String.IsNullOrEmpty(price)) {
                throw new Exception("Price field is empty. Change this and try again.");
            }

            double value = Double.Parse(price);
            if(value <= 0) {
                throw new Exception("Price field have invalid value. Change this and try again.");
            }

            return value;
        }

        public int GetQuantityField()
        {
            string quantity = quantityText.Text;
            if (String.IsNullOrEmpty(quantity)) {
                throw new Exception("Quantity field is empty. Change this and try again.");
            }

            int value = Int32.Parse(quantity);
            if (value <= 0) {
                throw new Exception("Quantity field have invalid value. Change this and try again.");
            }

            return value;
        }

        public Interval GetLimitFields()
        {
            int from = Int32.Parse(limitFromText.Text);
            int till = Int32.Parse(limitTillText.Text);
            if(from >= 0 && till < 100 && from < till) {
                return new Interval(from, till);
            }
            else {
                throw new Exception("Limit fields wrong. Change this and try again.");
            }
        }
        #endregion

        #region Event Handlers
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.ADD_COMMAND);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.REMOVE_COMMAND);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.SHOW_COMMAND);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.CLEAR_COMMAND);
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.FIND_COMMAND);
        }

        private void FindCheapest_Click(object sender, RoutedEventArgs e)
        {
            InvokeCommandByName(Constants.CHEAPEST_COMMAND);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Constants.ABOUT_INFO, "About authors", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                string title = GetTitleField();
                Book book = viewModel.Context.GetBookByTitle(title);

                if (book != null) {
                    Add.Content = "Update";
                    IsUpdate = true;
                }
                else {
                    Add.Content = "Add";
                    IsUpdate = false;
                }
            }
            catch(Exception ex) {

            }
        }
        #endregion

        #region Private Methods
        private void InvokeCommandByName(string value)
        {
            try {
                ICommandButton command = viewModel.GetCommandByName(value);
                command.Execute();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}
