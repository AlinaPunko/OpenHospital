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
                cmd.Parameters.Add("login", username);
                OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
                cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
                var reader = cmd.ExecuteReader();
                var dt = cmd.ExecuteReader();
                if (dt.Read())
                {
                    User user = new User()
                    {
                        ID = Convert.ToInt32(dt["ID"]),
                        Login = dt["Login"].ToString(),
                        Password = dt["Password"].ToString(),
                        DoctorID = dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                        PatientID = dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                        RoleID = Convert.ToInt32(dt["RoleID"])
                    };
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
                OracleCommand cmd = new OracleCommand("GetUserByID", App.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", userId);
                OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
                cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
                var dt = cmd.ExecuteReader();
                if (dt.Read())
                {
                    User user = new User()
                    {
                        ID = Convert.ToInt32(dt["ID"]),
                        Login = dt["Login"].ToString(),
                        Password = dt["Password"].ToString(),
                        DoctorID = dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                        PatientID = dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                        RoleID = Convert.ToInt32(dt["RoleID"])
                    };
                    return user;
                }
                else return null;
            
        }

        /// <summary>
        /// Validates login details
        /// </summary>
        /// <param name="username">Username of the user to log in</param>
        /// <param name="password">Password of the user to logi in</param>
        /// <returns></returns>
        public static bool IsValidLoginData(string username, string password)
        {
                OracleCommand cmd = new OracleCommand("Login", App.con);
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
                cmd.Parameters.Add("login", username);
                cmd.Parameters.Add("password", password);
                cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
                //int result = Convert.ToInt32(cmd.ExecuteScalar());
                var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                User user = new User()
                {
                    ID = Convert.ToInt32(dt["ID"]),
                    Login = dt["Login"].ToString(),
                    Password = dt["Password"].ToString(),
                    DoctorID = dt["DoctorID"].ToString() == "" ? 0 : int.Parse(dt["DoctorID"].ToString()),
                    PatientID = dt["PatientID"].ToString() == "" ? 0 : int.Parse(dt["PatientID"].ToString()),
                    RoleID = Convert.ToInt32(dt["RoleID"])
                };
                if (user.RoleID == 2) {
                    try
                    {
                        App.con.Close();
                        App.con.ConnectionString = ConfigurationManager.ConnectionStrings["Doctor"].ConnectionString;
                        App.con.Open();
                        MessageBox.Show("Open");
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
            else if (user.RoleID == 3)
                {
                    try
                    {
                        App.con.Close();
                        App.con.ConnectionString = ConfigurationManager.ConnectionStrings["Patient"].ConnectionString;
                        App.con.Open();
                        MessageBox.Show("Open");
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
                return true;
                }
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
                    DoctorID = 0,
                    PatientID = 0,
                };
                return user;
            }
        }

        public static void InsertUser(User user)
        {
            OracleCommand cmd = new OracleCommand("Register", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("login", user.Login);
            cmd.Parameters.Add("password", user.Password);
            cmd.Parameters.Add("doctorid", user.DoctorID);
            cmd.Parameters.Add("patientid", user.PatientID);
            cmd.Parameters.Add("roleid", user.RoleID);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
        }

        public static void UpdateUser(User user)
        {
            //TherapistContainer1 context = new TherapistContainer1();
            //context.Users.AddObject(user);
            //context.ObjectStateManager.ChangeObjectState(user, System.Data.EntityState.Modified);
            //context.SaveChanges();
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
            OracleCommand cmd = new OracleCommand("DeleteUserById", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("userID",userId);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
        }
        public static void DeleteUserByDoctorId(int doctorId)
        {
            OracleCommand cmd = new OracleCommand("DeleteUserByDoctorId", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("doctorID", doctorId);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
        }
        public static void DeleteUserByPatientId(int patientId)
        {
            OracleCommand cmd = new OracleCommand("DeleteUserByPatientId", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("patientID", patientId);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
        }
    }
}
