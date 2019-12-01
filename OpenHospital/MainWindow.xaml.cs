using OpenHospital.Tables;
using OpenHospital.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OpenHospital
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public MainWindow()
        {
            InitializeComponent();
            UserName.Text = Membership.CurrentUser.Login.ToString();
            AppWindow = this;
            //Time.Content = DateTime.Now.TimeOfDay;
            if (Membership.CurrentUser.RoleID == 2)
            {
                //ItemAddDoctor.Visibility = Visibility.Collapsed;
                ItemAddAdmin.Visibility = Visibility.Collapsed;
                //ItemAddPatient.Visibility = Visibility.Collapsed;
                Doctorshex.Visibility = Visibility.Collapsed;
            }
            if (Membership.CurrentUser.RoleID == 3)
            {
                ItemDoctors.Visibility = Visibility.Collapsed;
                //ItemAddDoctor.Visibility = Visibility.Collapsed;
                ItemAddAdmin.Visibility = Visibility.Collapsed;
                //ItemAddPatient.Visibility = Visibility.Collapsed;
                MyInfo.Text = "Обо мне";
                ItemVisits.Visibility = Visibility.Collapsed;
                Doctorshex.Visibility = Visibility.Collapsed;
                
            }
        }

        private void MainForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }


        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemDoctors":
                    {
                        Doctors doctors = new Doctors();
                        ContentC.Content = doctors;
                        break;
                    }
                case "ItemPatients":
                    {
                        if (Membership.CurrentUser.RoleID == 3)
                        {
                            EditPatient editPatient = new EditPatient(Membership.CurrentUser.Patient.Id);
                            ContentC.Content = editPatient;
                        }
                        else
                        {
                            Patients patients = new Patients();
                            ContentC.Content = patients;
                        }
                        break;
                    }
                case "ItemVisits":
                    {
                         Visits visits = new Visits();
                         ContentC.Content = visits;
                         break;
                    }
                case "ItemAddDoctor":
                    {
                        EditDoctor newForm = new EditDoctor();
                        newForm.ShowDialog();
                        break;
                    }
                case "ItemAddAdmin":
                    {
                        EditUser newForm = new EditUser(0);
                        newForm.ShowDialog();
                        break;
                    }
                case "ItemAddPatient":
                    {
                        EditPatient editpatient = new EditPatient(/*0*/);
                        MainWindow.AppWindow.ContentC.Content = editpatient;
                        break;
                    }
                case "Statistics":
                    {
                        Statistics item = new Statistics();
                        MainWindow.AppWindow.ContentC.Content = item;
                        break;
                    }
                default:

                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Membership.LogOutUser();
            LoginWindow loginForm = new LoginWindow();
            loginForm.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window1 window = new Window1();
            //window.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.con.Close();
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            //EditPatientForm editPatientForm = new EditPatientForm(0);
            //editPatientForm.ShowDialog();
        }
        private void AddVisit(object sender, RoutedEventArgs e)
        {
            //EditVisitForm editVisitForm = new EditVisitForm(0);
            //editVisitForm.ShowDialog();
        }
        private void AddConsultation(object sender, RoutedEventArgs e)
        {
            //EditConsultationForm editConsultationForm = new EditConsultationForm(0);
            //editConsultationForm.ShowDialog();
        }
        private void Doctors(object sender, RoutedEventArgs e)
        {
            //DoctorsForm doctorsForm = new DoctorsForm();
            //doctorsForm.ShowDialog();
        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer();
            timer.Start();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) =>
            {
                GetTime.Content = DateTime.Now.ToString("HH:mm:ss");
                GetDate.Content = DateTime.Now.ToString("ddd MMM yyy");
            };

        }
    }
}
