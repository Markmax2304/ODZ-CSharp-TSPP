using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ODZ_TSPP.Utils;

namespace ODZ_TSPP
{
    public class Model
    {
        private MySqlConnection dbConnection;
        private static List<Book> books = new List<Book>();

        public Model()
        {
            try {
                ConnectToMySqlDB();
                dbConnection.Open();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        ~Model()
        {
            dbConnection.Close();
            dbConnection.Dispose();
        }

        #region Public Methods
        public List<Book> GetAllBooks()
        {
            string queryAllItems = "SELECT name, price, count, confines, idbook FROM books";

            return GetBooksList(queryAllItems);
        }

        public Book GetBookByTitle(string value)
        {
            string queryAllItems = $"select name, price, count, confines, idbook from books where NAME like '{value}'";
            List<Book> books = GetBooksList(queryAllItems);
            if (books != null && books.Count > 0)
            {
                return books[0];
            }

            return null;
        }

        public void AddBook(Book book)
        {
            if(GetBookByTitle(book.Title) != null) 
            {
                throw new Exception($"Such book {book.Title} is already existed in DataBase.");
            }

            string insertQuery = String.Format("INSERT INTO books (name, price, count, confines) VALUES ('{0}', '{1}', '{2}', '{3}-{4}')",
                book.Title, book.Price, book.Quantity, book.Limit.from, book.Limit.till);

            MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateBook(Book book)
        {
            if (GetBookByTitle(book.Title) == null) {
                throw new Exception($"Such book {book.Title} is not existed in DataBase.");
            }

            string insertQuery = String.Format("UPDATE books SET price = '{0}', count = '{1}', confines = '{2}-{3}' WHERE name = '{4}'",
                book.Price, book.Quantity, book.Limit.from, book.Limit.till, book.Title);

            MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteBook(string title)
        {
            if (GetBookByTitle(title) == null)
            {
                throw new Exception($"Such book {title} is not existed in DataBase.");
            }

            string deleteQuery = String.Format("DELETE FROM books WHERE name = '{0}'", title);

            MySqlCommand cmd = new MySqlCommand(deleteQuery, dbConnection);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Private Methods
        private void ConnectToMySqlDB()
        {
            string path = Directory.GetCurrentDirectory();
            string config;
            using (FileStream fstream = File.OpenRead($"{path}\\config.txt"))
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                config = Encoding.Default.GetString(array);
            }

            string[] configs = config.Split('/');
            string[] parameters = new string[configs.Length];
            for (int i = 0; i < configs.Length; i++)
            {
                string[] row = configs[i].Split('=');
                parameters[i] = row[1];
            }

            dbConnection = DBUtils.GetDBConnection(
                parameters[0], parameters[1], parameters[2], parameters[3]);
        }

        private List<Book> GetBooksList(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, dbConnection);
            List<Book> dbBooks = new List<Book>();

            using (DbDataReader dbReader = cmd.ExecuteReader()) {
                if (dbReader.HasRows) {
                    while (dbReader.Read()) {
                        string title = dbReader.GetString(0);
                        double price = Convert.ToDouble(dbReader.GetValue(1));
                        int quantity = Convert.ToInt32(dbReader.GetValue(2));
                        string[] confines = dbReader.GetString(3).Split('-');
                        Interval limit = new Interval(Int32.Parse(confines[0]), Int32.Parse(confines[1]));
                        int id = Convert.ToInt32(dbReader.GetValue(4));

                        dbBooks.Add(new Book(title, price, quantity, limit));
                    }
                }
            }

            return dbBooks;
        }
        #endregion
    }
}
