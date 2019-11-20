using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OpenHospital
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OracleConnection con;
        public void setConnection()
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["Admin"].ConnectionString;
            con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
                //MessageBox.Show("Open");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public App()
        {
            InitializeComponent();
            setConnection();            
        }
    }
}
