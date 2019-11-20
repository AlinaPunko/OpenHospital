using OpenHospital.Data;
using OpenHospital.Model;
using OpenHospital.UserControls;
using System;
using System.Collections.Generic;
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

namespace OpenHospital.Tables
{
    /// <summary>
    /// Логика взаимодействия для Visits.xaml
    /// </summary>
    public partial class Visits : UserControl
    {
        public Visits()
        {
            InitializeComponent();
            GetAllVisits();
        }

        private void GetAllVisits()
        {
            if(Membership.CurrentUser.RoleID==1)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisits();
            else if (Membership.CurrentUser.RoleID == 2)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByDoctorId(Membership.CurrentUser.Doctor.Id);
            else if (Membership.CurrentUser.RoleID == 3)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByPatientId(Membership.CurrentUser.Patient.Id);//throw new NotImplementedException();
        }

        private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //this.Presenter.LoadConsultationsByCriterias();
        }
        public Visits(bool choose = true)
        {
            //InitializeComponent();
            //this.Presenter = new ConsultationsPresenter(this);
            ////this.Presenter.LoadConsultationsByCriterias();
            //this.Presenter.LoadAllConsultations();
        }
        //public ConsultationsPresenter Presenter { get; set; }



        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            //this.Presenter.LoadConsultationsByCriterias();
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

            var editVisit = new EditVisit();
            MainWindow.AppWindow.ContentC.Content =editVisit;
            //this.Presenter.LoadConsultationsByCriterias();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var editVisit = new EditVisit(Convert.ToInt32(((System.Data.DataRowView)dataGridViewResult.SelectedItems[0]).Row.ItemArray[0]));
            MainWindow.AppWindow.ContentC.Content = editVisit;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
            if (row == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить этот визит ", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                //var patient = (Patient)row;
                //int patientId = patient.Id;
                Doctor doctor = new Doctor();
                doctor = DoctorDataAccess.SelectDoctorByName(row.Row.ItemArray[0].ToString());
                VisitsDataAccess.DeleteVisitById(doctor.Id, Convert.ToDateTime(row.Row.ItemArray[2].ToString()));
                if (Membership.CurrentUser.RoleID == 1)
                    dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisits();
                else if (Membership.CurrentUser.RoleID == 2)
                    dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByDoctorId(Membership.CurrentUser.Doctor.Id);
                else if (Membership.CurrentUser.RoleID == 3)
                    dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByPatientId(Membership.CurrentUser.Patient.Id);//throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                //this.Message = errorMessage;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        public static implicit operator Frame(Visits v)
        {
            throw new NotImplementedException();
        }
    }
}
