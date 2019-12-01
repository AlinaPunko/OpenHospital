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
        public bool SelectFlag = false;
        public Visits()
        {
            InitializeComponent();
            GetAllVisits();
            dataGridViewResult.AutoGenerateColumns = true;
            List<string> types = new List<string>();
            types.Add("");
            types.Add("Первичный");
            types.Add("Вторичный");
            types.Add("Обследование");
            VisitType.ItemsSource = types;
            if (Membership.CurrentUser.RoleID ==3)
                panelButtons.Visibility = Visibility.Collapsed;
        }

        private void GetAllVisits()
        {
            if(Membership.CurrentUser.RoleID==1)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisits();
            else if (Membership.CurrentUser.RoleID == 2)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByDoctorId(Membership.CurrentUser.Doctor.Id);
            else if (Membership.CurrentUser.RoleID == 3)
                dataGridViewResult.ItemsSource = VisitsDataAccess.GetVisitsByPatientId(Membership.CurrentUser.Patient.Id);//throw new NotImplementedException();
            var list = dataGridViewResult.Columns;
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
            dataGridViewResult.Columns.Clear();
            List<Visit> visits = new List<Visit>();
            if (Membership.CurrentUser.RoleID == 1)
            {
                for (int i = 0; i < VisitsDataAccess.GetVisits().Count; i++)
                {
                    var row = VisitsDataAccess.GetVisits()[i];
                    visits.Add(new Visit(Convert.ToInt32(row.Row.ItemArray[0]),
                    DoctorDataAccess.GetDoctorByName(row.Row.ItemArray[1].ToString()),
                    PatientsDataAccess.GetPatientByName(row.Row.ItemArray[2].ToString()),
                    Convert.ToDateTime(row.Row.ItemArray[3]),
                    VisitsDataAccess.GetTypeByName(row.Row.ItemArray[4].ToString()),
                    row.Row.ItemArray[5].ToString(),
                    row.Row.ItemArray[6].ToString(),
                    row.Row.ItemArray[7].ToString(),
                    row.Row.ItemArray[8].ToString(),
                    new Room(row.Row.ItemArray[9].ToString(), null),
                    (row.Row.ItemArray[10].ToString() == "") ? null : (byte[])row.Row.ItemArray[10]
                    ));

                }
            }
            else if(Membership.CurrentUser.RoleID==2)
                for (int i = 0; i < VisitsDataAccess.GetVisitsByDoctorId(Membership.CurrentUser.Doctor.Id).Count; i++)
                {
                    var row = VisitsDataAccess.GetVisitsByDoctorId(Membership.CurrentUser.Doctor.Id)[i];
                    visits.Add(new Visit(Convert.ToInt32(row.Row.ItemArray[0]),
                    DoctorDataAccess.GetDoctorByName(row.Row.ItemArray[1].ToString()),
                    PatientsDataAccess.GetPatientByName(row.Row.ItemArray[2].ToString()),
                    Convert.ToDateTime(row.Row.ItemArray[3]),
                    VisitsDataAccess.GetTypeByName(row.Row.ItemArray[4].ToString()),
                    row.Row.ItemArray[5].ToString(),
                    row.Row.ItemArray[6].ToString(),
                    row.Row.ItemArray[7].ToString(),
                    row.Row.ItemArray[8].ToString(),
                    new Room(row.Row.ItemArray[9].ToString(), null),
                    (row.Row.ItemArray[10].ToString() == "") ? null : (byte[])row.Row.ItemArray[10]
                    ));

                }
            dataGridViewResult.AutoGenerateColumns = false;

            DataGridTextColumn id = new DataGridTextColumn();
            id.Header = "ID";
            id.Binding = new Binding("ID");
            dataGridViewResult.Columns.Add(id);

            DataGridTextColumn doctor = new DataGridTextColumn();
            doctor.Header = "Доктор";
            doctor.Binding = new Binding("Doctor.Name");
            dataGridViewResult.Columns.Add(doctor);

            DataGridTextColumn patient = new DataGridTextColumn();
            patient.Header = "Пациент";
            patient.Binding = new Binding("Patient.Name");
            dataGridViewResult.Columns.Add(patient);

            DataGridTextColumn datetime = new DataGridTextColumn();
            datetime.Header = "Дата/время";
            datetime.Binding = new Binding("DateTime");
            dataGridViewResult.Columns.Add(datetime);

            DataGridTextColumn type = new DataGridTextColumn();
            type.Header = "Тип";
            type.Binding = new Binding("Type.Type");
            dataGridViewResult.Columns.Add(type);

            DataGridTextColumn sympthoms = new DataGridTextColumn();
            sympthoms.Header = "Симптомы";
            sympthoms.Binding = new Binding("Symthoms");
            dataGridViewResult.Columns.Add(sympthoms);

            DataGridTextColumn diagnosis = new DataGridTextColumn();
            diagnosis.Header = "Диагноз";
            diagnosis.Binding = new Binding("Diagnosis");
            dataGridViewResult.Columns.Add(diagnosis);

            DataGridTextColumn prescription = new DataGridTextColumn();
            prescription.Header = "Назначения";
            prescription.Binding = new Binding("Prescription");
            dataGridViewResult.Columns.Add(prescription);

            DataGridTextColumn notes = new DataGridTextColumn();
            notes.Header = "Заметки";
            notes.Binding = new Binding("Notes");
            dataGridViewResult.Columns.Add(notes);

            DataGridTextColumn room = new DataGridTextColumn();
            room.Header = "Кабинет";
            room.Binding = new Binding("Room.Number");
            dataGridViewResult.Columns.Add(room);

            IEnumerable < Visit > result = visits;
            if(dateTimePickerFrom.SelectedDate!=null)
                result = result.Where(el => (el.DateTime>dateTimePickerFrom.SelectedDate)).Select(el => el);
            if (dateTimePickerTo.SelectedDate != null)
                result = result.Where(el => (el.DateTime < dateTimePickerTo.SelectedDate)).Select(el => el);
            if(!String.IsNullOrEmpty(Patient.Text))
                result = result.Where(el => (el.Patient.Name.Contains(Patient.Text))).Select(el => el);
            if (!String.IsNullOrEmpty(Doctor.Text))
                result = result.Where(el => (el.Doctor.Name.Contains(Doctor.Text))).Select(el => el);
            if (!String.IsNullOrEmpty(Diagnosis.Text))
                result = result.Where(el => (el.Diagnosis.Contains(Patient.Text))).Select(el => el);
            if( VisitType.SelectedItem != null && VisitType.SelectedItem.ToString()!="" )
                result = result.Where(el => (el.Type.Type == VisitType.SelectedItem.ToString())).Select(el => el);
            dataGridViewResult.ItemsSource = result;
            SelectFlag = true;

        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

            var editVisit = new EditVisit();
            MainWindow.AppWindow.ContentC.Content =editVisit;
            //this.Presenter.LoadConsultationsByCriterias();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectFlag == false)
            { var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
              var editVisit = new EditVisit(Convert.ToInt32((row.Row.ItemArray[0].ToString())));
                MainWindow.AppWindow.ContentC.Content = editVisit;
            }

            else
            { var row = (Visit)dataGridViewResult.SelectedItems[0];
                var editVisit = new EditVisit(row.ID);
                MainWindow.AppWindow.ContentC.Content = editVisit;
            }
                       

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
                VisitsDataAccess.DeleteVisitById(Convert.ToInt32((row.Row.ItemArray[0].ToString())));
                GetAllVisits();

            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                MessageBox.Show(errorMessage);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

    }
}
