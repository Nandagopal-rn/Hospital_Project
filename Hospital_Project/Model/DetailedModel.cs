﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Project.Model
{
    public class DetailedModel
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
