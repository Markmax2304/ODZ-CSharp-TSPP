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
        List<Book> books;

        public Model()
        {
            books = new List<Book>()
            {
                new Book("Lord of the rings", 10.0, 150, new Interval(14, 50)),
                new Book("War and piece", 20.9, 40, new Interval(21, 50))
            };

            //ConnectToMySqlDB();
            //dbConnection.Open();
        }

        ~Model()
        {
            dbConnection.Close();
            dbConnection.Dispose();
        }

        #region Public Methods
        public List<Book> GetAllBooks()
        {
            return books;

            string queryAllItems = "select name, price, count, confines from DB_books"; //change table name in DB 

            MySqlCommand cmd = new MySqlCommand(queryAllItems, dbConnection);
            List<Book> dbBooks = new List<Book>();

            using (DbDataReader dbReader = cmd.ExecuteReader())
            {
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        string title = dbReader.GetString(0);
                        double price = Convert.ToDouble(dbReader.GetValue(1));
                        int quantity = Convert.ToInt32(dbReader.GetValue(2));
                        string[] confines = dbReader.GetString(3).Split('-');
                        Interval limit = new Interval(Int32.Parse(confines[0]), Int32.Parse(confines[1]));

                        dbBooks.Add(new Book(title, price, quantity, limit));
                    }
                }
            }

            return dbBooks;
        }

        public Book GetBookByTitle(string value)
        {
            Book book = books.Find(x => x.Title == value);
            if(book != null)
            {
                return book;
            }
            throw new Exception($"Such book {value} dont exist in DataBase.");
        }

        public void AddBook(Book book)
        {
            if (books.Contains(book))
            {
                throw new Exception($"Such book {book.Title} is already existed in DataBase.");
            }
            books.Add(book);
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

            string[] configs = config.Split('\n');
            string[] parameters = new string[configs.Length];
            for (int i = 0; i < configs.Length; i++)
            {
                string[] row = configs[i].Split('=');
                parameters[i] = row[1].Substring(0, row[1].Length - 1);
            }

            dbConnection = DBUtils.GetDBConnection(
                parameters[0], Int32.Parse(parameters[1]), parameters[2], parameters[3], parameters[4]);
        }
        #endregion
    }
}
