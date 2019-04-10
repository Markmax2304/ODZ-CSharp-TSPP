using System.Collections.Generic;
using ODZ_TSPP.Models;

namespace ODZ_TSPP.DB
{
    public interface IDataAccess
    {
        void SaveToDB(List<IDatable> books);
        List<IDatable> LoadFromDB();
    }
}
