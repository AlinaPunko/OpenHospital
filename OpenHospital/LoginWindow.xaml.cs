using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace OpenHospital
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        OracleConnection con;
        public LoginWindow()
        {
            InitializeComponent();
            this.setConnection();
        }
        private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TryLogin();
        }
        private void LoginForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }
        private void setConnection()
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();               
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void TryLogin()
        {
            string username = UserName.Text;
            string password = UserPassword.Password;

            string loginResultMessage = string.Empty;
            if (TryLogin(username, password, out loginResultMessage))
            {
                MessageBox.Show("Success");
                
                //MainWindow a = new MainWindow();
                //a.Show();
                //this.Close();
            }
            else
            {
                UserName.Text = null;
                UserPassword.Password = null;
                labelMessage.Content = loginResultMessage;
                labelMessage.Foreground = Brushes.Red;
                labelMessage.BorderBrush = Brushes.Red;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
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
        private bool TryLogin(string username, string password, out string message)
        {
            
            //dataReader.Close();
            message = "Ошибка при входе. Обратитесь к администратору!";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Users";
            cmd.CommandType = CommandType.Text;
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(cmd.CommandText, con);
            OracleDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            var User = from myrow in dt.AsEnumerable() where (string)myrow["Login"] == username select myrow;
            //    return false;
            //}

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Window1 window = new Window1();
            //window.ShowDialog();
        }
    }
    public class NewCustomCommand
    {
        private static RoutedUICommand enterCommand;
        static NewCustomCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Enter));
            enterCommand = new RoutedUICommand("PNV1", "PNV1", typeof(NewCustomCommand), inputs);

        }
        public static RoutedUICommand EnterCommand
        {
            get { return enterCommand; }
        }

    }

}
