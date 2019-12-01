using OpenHospital.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

namespace OpenHospital.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
            try
            {
                //Doctors.Text += StatisticsAccess.countdoctors();
                //Patients.Text += StatisticsAccess.countpatients();
                //Visits.Text += StatisticsAccess.countvisits();
                //Specialities.Text += StatisticsAccess.countspec();
                //Rooms.Text += StatisticsAccess.countrooms();
                //Second.Text += StatisticsAccess.countsecond();
                //First.Text += StatisticsAccess.countfirst();
                //Highest.Text += StatisticsAccess.counthigh();
                //VisitsMonth.Text += StatisticsAccess.visitslastmonth();
                //Childen.Text += StatisticsAccess.countchildren();
                //Old.Text += StatisticsAccess.countretiree();
            }
            catch(Exception e)
            {
                MessageBox.Show("Упс, ошибка");
            }
            
        }

        private void XMLexport_Click(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand("exportpatients", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if(dt.Read())
            {
                using (FileStream fs = new FileStream("patients.xml", FileMode.Create))
                {
                    fs.Write(Encoding.Unicode.GetBytes(dt[0].ToString()), 0, Encoding.Unicode.GetBytes(dt[0].ToString()).Length);
            }
            }
            cmd.Dispose();
            OracleCommand cmd1 = new OracleCommand("exportvisits", App.con);
            OracleParameter user_par1 = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd1.Parameters.Add(user_par1).Direction = System.Data.ParameterDirection.Output;
            var dt1 = cmd1.ExecuteReader();
            if (dt1.Read())
            {
                using (FileStream fs = new FileStream("visits.xml", FileMode.Create))
                {
                    fs.Write(Encoding.Unicode.GetBytes(dt1[0].ToString()), 0, dt1[0].ToString().Length);
                }
            }

            OracleCommand cmd2 = new OracleCommand("exportdoctors", App.con);
            cmd2.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par2 = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd2.Parameters.Add(user_par2).Direction = System.Data.ParameterDirection.Output;
            var dt2 = cmd2.ExecuteReader();
            if (dt2.Read())
            {
                using (FileStream fs = new FileStream("doctors.xml", FileMode.Create))
                {
                    fs.Write(Encoding.Unicode.GetBytes(dt2[0].ToString()), 0, Encoding.Unicode.GetBytes(dt2[0].ToString()).Length);
                }
            }
            dt.Close();

            OracleCommand cmd3 = new OracleCommand("exportusers", App.con);
            cmd3.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par3 = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd3.Parameters.Add(user_par3).Direction = System.Data.ParameterDirection.Output;
            var dt3 = cmd3.ExecuteReader();
            if (dt3.Read())
            {
                using (FileStream fs = new FileStream("users.xml", FileMode.Create))
                {
                    fs.Write(Encoding.Unicode.GetBytes(dt3[0].ToString()), 0, Encoding.Unicode.GetBytes(dt3[0].ToString()).Length);
                }
            }
            dt.Close();
        }

        private void XMLImport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
