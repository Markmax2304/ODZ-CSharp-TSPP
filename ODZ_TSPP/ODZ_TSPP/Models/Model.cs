using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ODZ_TSPP.DB;

namespace ODZ_TSPP
{
    public class Model
    {
        private MySqlConnection dbConnection;
        private static List<Book> books = new List<Book>();

        public Model()
        {
            ConnectToMySqlDB();
            dbConnection.Open();
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
            Book book = GetBooksList(queryAllItems)[0];

            return book;
        }

        public void AddBook(Book book)
        {
            if(GetBookByTitle(book.Title) != null) 
            {
                throw new Exception($"Such book {book.Title} is already existed in DataBase.");
            }

            string insertQuery = String.Format("INSERT INTO books VALUES ('{0}', '{1}', '{2}', '{3}-{4}', {5})",
                book.Title, book.Price, book.Quantity, book.Limit.from, book.Limit.till, book.Id);

            MySqlCommand cmd = new MySqlCommand(insertQuery, dbConnection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteBook(Book book)
        {
            if (!books.Contains(book))
            {
                throw new Exception($"Such book {book.Title} is not existed in DataBase.");
            }
            books.Remove(book);
        }

        public void DeleteBook(string title)
        {
            Book book = books.Find(x => x.Title == title);
            if (books == null)
            {
                throw new Exception($"Such book {book.Title} is not existed in DataBase.");
            }
            books.Remove(book);
        }
        #endregion

        #region Private Methods
        // check!
        private void ConnectToMySqlDB()
        {
            //dbConnection = DBUtils.GetDBConnection("localhost", "root", "tspp", "jeka052"); 

            string path = Directory.GetCurrentDirectory();
            string config;
            using (FileStream fstream = File.OpenRead($"{path}\\config.txt"))
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                config = Encoding.Default.GetString(array);
            }

            string[] configs = config.Split('\n');
            string[] parameters = new string[configs.Length];
            for (int i = 0; i < configs.Length; i++)
            {
                string[] row = configs[i].Split('=');
                parameters[i] = row[1].Substring(0, row[1].Length - 1);
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

                        dbBooks.Add(new Book(id, title, price, quantity, limit));
                    }
                }
            }

            return dbBooks;
        }
        #endregion
    }
}
