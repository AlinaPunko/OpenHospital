using OpenHospital.Data;
using OpenHospital.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenHospital
{
    class Membership
    {
        private static User _currentUser;

        /// <summary>
        /// Currently logged user
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = UsersDataAccess.AnonimousUser;
                }

                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }

        /// <summary>
        /// Checks if login details are correct
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidLoginDetails(string username, string password)
        {
            bool isValid = UsersDataAccess.IsValidLoginData(username, GetHashString(password));
            return isValid;
        }
        public static string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
        /// <summary>
        /// Validates login details and logs in the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateAndLogin(string username, string password)
        {
            if (IsValidLoginDetails(username, password) == false)
            {
                return false;
            }

            var user = UsersDataAccess.GetUserByName(username);
            LogInUser(user);

            if (CurrentUser.Login == username)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Logs in passed user
        /// </summary>
        /// <param name="user"></param>
        public static void LogInUser(User user)
        {
            CurrentUser = user ?? throw new ArgumentNullException("пользователь не должен иметь значение null!");
            if (user.RoleID == 2)
            {
                try
                {
                    App.con.Close();
                    App.con.ConnectionString = ConfigurationManager.ConnectionStrings["Doctor"].ConnectionString;
                    App.con.Open();
                    //MessageBox.Show("Open");
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
                    //MessageBox.Show("Open");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        /// <summary>
        /// Logs out curent user
        /// </summary>
        internal static void LogOutUser()
        {
            Membership.CurrentUser = UsersDataAccess.AnonimousUser;
        }
    }
}
