using MaterialDesignThemes.Wpf;
using OpenHospital.Data;
using OpenHospital.Model;
using OpenHospital.UserControls;
using System;
using System.Collections;
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
    /// Логика взаимодействия для Patients.xaml
    /// </summary>
    public partial class Patients : UserControl//, IPatientsView
    {
        private IEnumerable<Patient> collection;

        public Patients()
        {
            InitializeComponent();
            
            dataGridViewResult.ItemsSource = PatientsDataAccess.GetPatients();
            if (Membership.CurrentUser.RoleID == 2)
            {
                buttonAdd.Visibility = Visibility.Collapsed;
                buttonDelete.Visibility = Visibility.Collapsed;
            }

        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //this.Presenter.LoadPatientsByCriterias();
        }
        public string NameSearch
        {
            get
            {
                return Name.Text;
            }
            set
            {
                this.Name.Text = value;
            }
        }
        public string AddressSearch
        {
            get
            {
                return Address.Text;
            }
            set
            {
                this.Address.Text = value;
            }
        }
        public DateTime BirthdateSearchTo
        {
            get
            {
                return dateTimePickerTo.DisplayDate;
            }
            set
            {
                this.dateTimePickerTo.DisplayDate = value;
            }
        }
        public DateTime BirthdateSearchFrom
        {
            get
            {
                return dateTimePickerFrom.DisplayDate;
            }
            set
            {
                this.dateTimePickerFrom.DisplayDate= value;
            }
        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            collection = from t in PatientsDataAccess.GetPatients() select t;
            if (!String.IsNullOrEmpty(Name.Text))
            {
                collection = from t in collection where t.Name.Contains(Name.Text) select t;
            }
            if (!String.IsNullOrEmpty(Address.Text))
                collection = from t in collection where t.Address.Contains(Address.Text) select t;
            if (dateTimePickerFrom.SelectedDate != null)
                collection = from t in collection where t.Birthdate > BirthdateSearchFrom select t;
            if (dateTimePickerTo.SelectedDate != null)
                collection = from t in collection where t.Birthdate < BirthdateSearchTo select t;
            dataGridViewResult.ItemsSource = collection;
            return;

        }
        private void PatientsForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //    this.DragMove();
        }
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var row = (Patient)dataGridViewResult.SelectedItem;
            //var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
            //var patient = GetSelectedPatient();
            if (row == null)
            {
                return;
            }

            //int patientId = patient.PatientID;
            EditPatient editpatient = new EditPatient(row.Id);
            MainWindow.AppWindow.ContentC.Content = editpatient;
               // App..Current.MainWindow= editpatient;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            
            EditPatient editpatient = new EditPatient(/*0*/);
            MainWindow.AppWindow.ContentC.Content = editpatient;
            //((MainWindow)Application.Current.MainWindow).ContentC.Content = editpatient;
        }


        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = (Patient)dataGridViewResult.SelectedItem;
            if (row == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить этого пациента ? ", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                //var patient = (Patient)row;
                //int patientId = patient.Id;
                UsersDataAccess.DeleteUserByPatientId(row.Id);
                dataGridViewResult.ItemsSource = PatientsDataAccess.GetPatients();

            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                //this.Message = errorMessage;
                MessageBox.Show(errorMessage);
            }
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult;//.OK
            //this.Close();
        }

        public bool TryChoosePatient(out Patient patient)
        {
            var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
            //this.panelButtons.Visibility = Visibility.Hidden;//cкрывает кнопку false
            //this.panelChooseButtons.Visibility = Visibility.Visible;//visible видимый
            if (row == null)
            {
                patient = null;
                return false;
            }
            Patient selectedpatient = new Patient(Convert.ToInt32(row.Row.ItemArray[0]), row.Row.ItemArray[1].ToString()
                , Convert.ToDateTime(row.Row.ItemArray[2]), row.Row.ItemArray[3].ToString(), row.Row.ItemArray[4].ToString());
            patient = selectedpatient;

            return true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult;//.Cancel
            //this.Close();
        }

        //private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    this.Close();
        //}
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.Presenter.LoadPatientsByCriterias();
        }
    }
}
