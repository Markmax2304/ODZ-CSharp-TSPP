using System;
using System.Collections.Generic;
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

        public Model()
        {
            string path = Directory.GetCurrentDirectory();
            string config;
            using (FileStream fstream = File.OpenRead($"{path}\\config.txt")) {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                config = Encoding.Default.GetString(array);
            }

            string[] configs = config.Split('\n');
            string[] parameters = new string[configs.Length];
            for(int i = 0; i < configs.Length; i++) {
                string[] row = configs[i].Split('=');
                parameters[i] = row[1].Substring(0, row[1].Length-1);
            }

            dbConnection = DBUtils.GetDBConnection(
                parameters[0], Int32.Parse(parameters[1]), parameters[2], parameters[3], parameters[4]);
        }

        public List<Book> GetAllBooks()
        {
            throw new Exception();
        }
    }
}
