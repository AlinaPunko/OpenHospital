using OpenHospital.Tables;
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
        public MainWindow()
        {
            InitializeComponent();
            //UserName.Text = Membership.CurrentUser.UserName.ToString();
            ////Time.Content = DateTime.Now.TimeOfDay;
            //if (Membership.CurrentUser.RoleID == 2)
            //{
            //    ItemDoctors.Visibility = Visibility.Collapsed;
            //    ItemAddDoctor.Visibility = Visibility.Collapsed;
            //    ItemAddAdmin.Visibility = Visibility.Collapsed;
            //    ItemAddPatient.Visibility = Visibility.Collapsed;
            //    Doctorshex.Visibility = Visibility.Collapsed;
            //}

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
                        Content.Content = doctors;
                        break;
                    }
                case "ItemPatients":
                    {
                        Patients patients = new Patients();
                        Content.Content = patients;
                        break;
                    }
                case "ItemVisits":
                    {
                         Visits visits = new Visits();
                         Content.Content = visits;
                         break;
                    }
            //    case "ItemAddDoctor":
            //        {
            //            EditDoctorForm newForm = new EditDoctorForm(true);
            //            newForm.ShowDialog();
            //            break;
            //        }
            //    case "ItemAddAdmin":
            //        {
            //            EditUserForm newForm = new EditUserForm(0);
            //            newForm.ShowDialog();
            //            break;
            //        }
            //    case "ItemAddPatient":
            //        {
            //            EditPatientForm newForm = new EditPatientForm(0);
            //            newForm.ShowDialog();
            //            break;
            //        }
            //    default:
            //        break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Membership.LogOutUser();
            //LoginForm loginForm = new LoginForm();
            //loginForm.Show();
            //this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window1 window = new Window1();
            //window.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
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
