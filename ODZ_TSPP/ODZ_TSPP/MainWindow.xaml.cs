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

        public void ClearOutputField()
        {
            outputText.Items.Clear();
        }

        public string GetTitleField()
        {
            string name = nameText.Text;
            if (String.IsNullOrEmpty(name)) {
                throw new Exception("Name field wrong. Change this and try again.");
            }
            return name;
        }

        public double GetPriceField()
        {
            string price = priceText.Text;
            if (String.IsNullOrEmpty(price)) {
                throw new Exception("Price field wrong. Change this and try again.");
            }
            return Double.Parse(price);
        }

        public int GetQuantityField()
        {
            string quantity = quantityText.Text;
            if (String.IsNullOrEmpty(quantity)) {
                throw new Exception("Quantity field wrong. Change this and try again.");
            }
            return Int32.Parse(quantity);
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
            ICommandButton command = viewModel.GetCommandByName(Constants.ADD_COMMAND);
            command.Execute();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ICommandButton command = viewModel.GetCommandByName(Constants.REMOVE_COMMAND);
            command.Execute();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ICommandButton command = viewModel.GetCommandByName(Constants.EDIT_COMMAND);
            command.Execute();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            ICommandButton command = viewModel.GetCommandByName(Constants.SHOW_COMMAND);
            command.Execute();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ICommandButton command = viewModel.GetCommandByName(Constants.CLEAR_COMMAND);
            command.Execute();
        }
        #endregion
    }
}
