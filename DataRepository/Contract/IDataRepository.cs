using DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Contract
{
    public interface IDataRepository
    {
        IEnumerable<DataModel> GetAllDetails();

        DataModel GetDataById(int id);

        void InsertDetails(DataModel dataModel);
    }
}
