using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace OpenHospital.Model
{
    internal class User
    {
        int id;
        int doctorID;
        int patientID;
        string password;
        string login;
        int roleID;
        public int ID { get => id; set => id = value; }
        public int DoctorID { get => doctorID; set => doctorID = value; }
        public int PatientID { get => patientID; set => patientID = value; }
        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }
        public int RoleID { get => roleID; set=> roleID = value; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   doctorID == user.doctorID &&
                   patientID == user.patientID &&
                   password == user.password &&
                   login == user.login &&
                   roleID == user.roleID &&
                   ID == user.ID &&
                   DoctorID == user.DoctorID &&
                   PatientID == user.PatientID &&
                   Password == user.Password &&
                   Login == user.Login &&
                   RoleID == user.RoleID;
        }

        public static bool operator ==(User user1, User user2)
        {
            return EqualityComparer<User>.Default.Equals(user1, user2);
        }

        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }
    }
}
