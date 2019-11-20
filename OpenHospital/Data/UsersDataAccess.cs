using OpenHospital.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Windows;

namespace OpenHospital.Data
{
    class UsersDataAccess
    {

        /// <summary>
        /// Get all users in the database
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetAllUsers()
        {
            OracleCommand cmd = new OracleCommand("GetAllUsers");
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from SELECTALLUSERS";
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            return dt.AsDataView().Cast<User>();
        }

        // <summary>
        // Get user by name from the database
        // </summary>
        // <param name = "username" ></ param >
        // < returns ></ returns >
        public static User GetUserByName(string username)
        {
            OracleCommand cmd = new OracleCommand("GetUserByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("username", username);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            //var reader = cmd.ExecuteReader();
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                User user = new User()
                {
                    ID = Convert.ToInt32(dt["ID"]),
                    Login = dt["Login"].ToString(),
                    Password = dt["Password"].ToString(),
                    Doctor =   new Doctor(),// dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                    Patient = new Patient(),// dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                    RoleID = Convert.ToInt32(dt["RoleID"])
                };
                if (user.RoleID == 2)
                    user.Doctor = DoctorDataAccess.GetDoctorById(Convert.ToInt32(dt["DoctorID"].ToString()));
                else if (user.RoleID == 3)
                    user.Patient = PatientsDataAccess.GetPatientById(Convert.ToInt32(dt["PatientID"].ToString()));
                return user;
            }
            else return null;

        }

        /// <summary>
        /// Get user by name from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>       
        public static User GetUserById(int userId)
        {

            User user = new User();
            OracleCommand cmd = new OracleCommand("GetUserByID", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("userID", userId);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            while (dt.Read())
            {
                user = new User()
                {
                    ID = Convert.ToInt32(dt["ID"]),
                    Login = dt["Login"].ToString(),
                    Password = dt["Password"].ToString(),
                    //DoctorID = dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                    //PatientID = dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                    RoleID = Convert.ToInt32(dt["RoleID"])
                };
                if (user.RoleID == 2)
                    user.Doctor = DoctorDataAccess.GetDoctorById(Convert.ToInt32(dt["DoctorID"].ToString()));
                else if (user.RoleID == 3)
                    user.Patient = PatientsDataAccess.GetPatientById(Convert.ToInt32(dt["PatientID"].ToString()));
            }
            return user;
        }

        /// <summary>
        /// Validates login details
        /// </summary>
        /// <param name="username">Username of the user to log in</param>
        /// <param name="password">Password of the user to logi in</param>
        /// <returns></returns>
        public static bool IsValidLoginData(string username, string password)
        {
            if (App.con.State == ConnectionState.Closed)
                App.con.Open();
            User user = null;
            OracleCommand cmd = new OracleCommand("Login", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("username", username);
            cmd.Parameters.Add("userpassword", password);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            //int result = Convert.ToInt32(cmd.ExecuteScalar());
            var dt = cmd.ExecuteReader();
            //MessageBox.Show(dt.Depth.ToString());
            while (dt.Read())
            {              
                    user = new User()
                    {
                        ID = Convert.ToInt32(dt["ID"]),
                        Login = dt["Login"].ToString(),
                        Password = dt["Password"].ToString(),
                        //DoctorID = dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                        //PatientID = dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                        RoleID = Convert.ToInt32(dt["RoleID"])
                    };
                if (user.RoleID == 2)
                    user.Doctor = DoctorDataAccess.GetDoctorById(Convert.ToInt32(dt["DoctorID"].ToString()));
                else if (user.RoleID == 3)
                    user.Patient = PatientsDataAccess.GetPatientById(Convert.ToInt32(dt["PatientID"].ToString()));
            }
            if (user != null)
                return true;
           else return false;
        }
    
         
        

  
    

        public static User AnonimousUser
        {
            get
            {
                var user = new User()
                {
                    Login = "Anonimous",
                    ID = 0,
                    Doctor = null,
                    Patient = null,
                };
                return user;
            }
        }

        public static void InsertUser(User user)
        {
            OracleCommand cmd = new OracleCommand("Register", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("username", user.Login);
            cmd.Parameters.Add("userpassword", user.Password);
            if (user.Doctor == null)
                cmd.Parameters.Add("doctor", "");
            else cmd.Parameters.Add("doctor", DoctorDataAccess.GetDoctorByName(user.Doctor.Name));
            if (user.Patient==null)
                cmd.Parameters.Add("patient", "");
            else cmd.Parameters.Add("patient", PatientsDataAccess.GetPatientByName(user.Patient.Name));
            cmd.Parameters.Add("role", user.RoleID);
           
            int res = cmd.ExecuteNonQuery();
        }

        public static void UpdateUser(User user)
        {
            OracleCommand cmd = new OracleCommand("UpdateUser", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("username", user.Login);
            cmd.Parameters.Add("userpassword", user.Password);
            cmd.Parameters.Add("doctor", user.Doctor);
            cmd.Parameters.Add("patient", user.Patient);
            cmd.Parameters.Add("role", user.RoleID);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
        }

        public static void DeleteUser(User user)
        {
            //TherapistContainer1 context = new TherapistContainer1();
            //if (user.EntityState == System.Data.EntityState.Detached)
            //{
            //    context.Users.Attach(user);
            //}
            //context.Users.DeleteObject(user);
            //context.SaveChanges();
        }

        public static void DeleteUserById(int userId)
        {
            OracleCommand cmd = new OracleCommand("DeleteUser", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("userID",userId);
            
            var dt = cmd.ExecuteReader();
        }
        public static void DeleteUserByDoctorId(int doctorId)
        {
            OracleCommand cmd = new OracleCommand("DeleteUserByDoctorId", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("did", doctorId);
            
            var dt = cmd.ExecuteReader();
            DoctorDataAccess.DeleteDoctorById(doctorId);
        }
        public static void DeleteUserByPatientId(int patientId)
        {
            OracleCommand cmd = new OracleCommand("DeleteUserByPatientId", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("pid", patientId);
            
            var dt = cmd.ExecuteReader();
        }
    }
}
