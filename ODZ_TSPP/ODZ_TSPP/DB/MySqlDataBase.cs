using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODZ_TSPP.Models;

using MySql.Data.MySqlClient;

namespace ODZ_TSPP.DB
{
    class MySqlDataBase : IDataAccess
    {
        public MySqlDataBase()
        {

        }

        public List<IDatable> LoadFromDB()
        {
            throw new NotImplementedException();
        }

        public void SaveToDB(List<IDatable> books)
        {
            throw new NotImplementedException();
        }
    }
}
