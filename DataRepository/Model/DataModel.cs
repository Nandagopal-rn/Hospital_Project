using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Model
{
    public class DataModel
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public long Phone_Number { get; set; }
        public string Res_Address { get; set; }
        public string User_Password { get; set; }
        public string Confirm_Password { get; set; }
    }
}
