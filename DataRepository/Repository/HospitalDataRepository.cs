using DataRepository.Contract;
using DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataRepository.Repository
{
    public class HospitalDataRepository : IDataRepository
    {
        private readonly SqlConnection _sqlConnection;

        public HospitalDataRepository()
        {
            var myConnection = "Server=10.10.100.68\\SQL2016;Database=Dotnet_Core_Training;User ID=spsauser;Password=$P$@789#;";

            _sqlConnection = new SqlConnection(myConnection);

        }

        public IEnumerable<DataModel> GetAllDetails()
        {
            try
            {
                _sqlConnection.Open();

                var selectCommand = new SqlCommand("EXEC PROJECT_HOSPITAL_SHOW", _sqlConnection);

                var myOperation = selectCommand.ExecuteReader();

                var detailProperties = new List<DataModel>();

                while (myOperation.Read())
                {
                    detailProperties.Add(new DataModel
                    {
                        Id = (int)myOperation["ID"],
                        First_Name = (string)myOperation["FIRST_NAME"],
                        Last_Name = (string)myOperation["LAST_NAME"],
                        Gender = (string)myOperation["GENDER"],
                        Email = (string)myOperation["EMAIL"],
                        Phone_Number = (long)myOperation["PHONE_NUMBER"],
                        Res_Address = (string)myOperation["RESIDENT_ADDRESS"],
                        User_Password = (string)myOperation["USER_PASSWORD"],
                    });
                }
                return detailProperties;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public DataModel GetDataById(int id)
        {
            try
            {
                _sqlConnection.Open();

                var myCommand = new SqlCommand("EXEC PROJECT_HOSPITAL_SHOWBYID @ID", _sqlConnection);
                myCommand.Parameters.AddWithValue("ID", id);
                var viewCommand = myCommand.ExecuteReader();
                var listById = new List<DataModel>();

                while (viewCommand.Read())
                {
                    listById.Add(new DataModel
                    {
                        Id = (int)viewCommand["ID"],
                        First_Name = (string)viewCommand["FIRST_NAME"],
                        Last_Name = (string)viewCommand["LAST_NAME"],
                        Gender = (string)viewCommand["GENDER"],
                        Email = (string)viewCommand["EMAIL"],
                        Phone_Number = (long)viewCommand["PHONE_NUMBER"],
                        Res_Address = (string)viewCommand["RESIDENT_ADDRESS"],
                        User_Password = (string)viewCommand["USER_PASSWORD"],
                    });
                }

                return listById.FirstOrDefault();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void InsertDetails(DataModel dataModel)
        {

            try
            {
                _sqlConnection.Open();

                var myCommand = new SqlCommand("EXEC PROJECT_HOSPITAL_INSERT @first_name,@last_name, @gender, @email,@ph_number,@res_address,@user_password", _sqlConnection);

                myCommand.Parameters.AddWithValue("first_name", dataModel.First_Name);
                myCommand.Parameters.AddWithValue("last_name", dataModel.Last_Name);
                myCommand.Parameters.AddWithValue("gender", dataModel.Gender);
                myCommand.Parameters.AddWithValue("email", dataModel.Email);
                myCommand.Parameters.AddWithValue("ph_number", dataModel.Phone_Number);
                myCommand.Parameters.AddWithValue("res_address", dataModel.Res_Address);
                myCommand.Parameters.AddWithValue("user_password", dataModel.User_Password);

                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
