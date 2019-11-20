using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace OpenHospital.Model
{
    public class User
    {
        int id;
        Doctor doctorID;
        Patient patientID;
        string password;
        string login;
        int roleID;
        public int ID { get => id; set => id = value; }
        public Doctor Doctor { get => doctorID; set => doctorID = value; }
        public Patient Patient { get => patientID; set => patientID = value; }
        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }
        public int RoleID { get => roleID; set=> roleID = value; }

    }
}
