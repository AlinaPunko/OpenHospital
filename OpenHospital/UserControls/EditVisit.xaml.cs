using OpenHospital.Data;
using OpenHospital.Model;
using OpenHospital.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace OpenHospital.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EditVisit.xaml
    /// </summary>
    public partial class EditVisit : UserControl
    {
        public Visit Visit = new Visit();
        public Doctor doctor = new Doctor();
        public Patient patient = new Patient();
        bool Flag = false;

        
        protected void FillView()
        {
           
            textBoxDiagnosis.Text = Visit.Diagnosis;
            textBoxPrescription.Text = Visit.Prescription;
            textboxRoom.Text = Visit.Room.Number;
            textBoxNotes.Text = Visit.Notes;
            textBoxSymptoms.Text = Visit.Symthoms;
            dateTimePickerVisitDate.SelectedDate= Visit.DateTime.Date;
            dateTimePickerVisitTime.SelectedTime =Convert.ToDateTime( Visit.DateTime.TimeOfDay.ToString());
            textBoxPatientName.Text = Visit.Patient.Name;
            textBoxDoctorName.Text = Visit.Doctor.Name;
            if (Visit.file == null)
                buttonShow.Visibility = Visibility.Hidden;
        }

        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            if (!isValid)
            {
                //View.Message = message;
            }

            return isValid;
        }

        protected bool IsDataValid(out string message)
        {
            message = string.Empty;
            bool isValid = true;

            if (String.IsNullOrEmpty(dateTimePickerVisitDate.DisplayDate.ToString()))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Дата");
                isValid = false;
            }
            if (String.IsNullOrEmpty(dateTimePickerVisitTime.ToString()))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Время");
                isValid = false;
            }
            if (String.IsNullOrEmpty(textBoxPatientName.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Пациент");
                isValid = false;
            }

            if (String.IsNullOrEmpty(textBoxDoctorName.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Врач");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Type.SelectedValue.ToString()))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Тип");
                isValid = false;
            }
            if (String.IsNullOrEmpty(textboxRoom.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Кабинет");
                isValid = false;
            }
            return isValid;
        }

        public void Save()
        {
            //this.FillVisit();
            bool isValid = IsValid();
            if (isValid)
            {
                Visit.Diagnosis = textBoxDiagnosis.Text;
                Visit.Prescription = textBoxPrescription.Text;
                Visit.Symthoms = textBoxSymptoms.Text;
                Visit.DateTime = dateTimePickerVisitDate.SelectedDate.Value.AddHours(dateTimePickerVisitTime.SelectedTime.Value.Hour).AddMinutes(dateTimePickerVisitTime.SelectedTime.Value.Minute); ;
                Visit.Notes = textBoxNotes.Text;
                Visit.Room = new Room(textboxRoom.Text, null);
                Visit.Type = VisitsDataAccess.GetTypeByName(Type.SelectedValue.ToString());
                SaveModel(Visit);
            }
        }

        private void SaveModel(Visit model)
        {
            try
            {
                if (Flag ==false)
                {
                    VisitsDataAccess.InsertVisit(Visit);
                }
                else
                {
                    VisitsDataAccess.UpdateVisit(Visit);
                }
                //View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища! Позвоните администратору!/n {0} ", e.Message);
                MessageBox.Show(message);
            }

        }

        public void CreateNew()
        {
            var newVisit = new Visit();
            this.Visit = newVisit;

            var currentUser = Membership.CurrentUser;

            var currentUserDoctor = currentUser.Doctor;
            if (currentUserDoctor != null)
            {
                this.Visit.Doctor.Id = currentUserDoctor.Id;
                //this.View.DoctorName = currentUserDoctor.Name;
            }

            //this.FillView();
        }

        public void Load(int visit)
        {
            try
            {
                if (visit==0)
                {
                    throw new ArgumentNullException("visit должен отличаться от 0!");
                }
                Visit = VisitsDataAccess.GetVisitByID(visit);
                this.FillView();
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
                MessageBox.Show(message);
            }
        }
        public EditVisit()
        {
            InitializeComponent();
            Visit.Patient = patient;
            Visit.Doctor = doctor;
            List<string> data = new List<string>();
            data.Add("Первичный");
            data.Add("Вторичный");
            data.Add("Обследование");
            Type.ItemsSource = data;

            //this.Presenter = new EditVisitPresenter(this);
            if (Membership.CurrentUser.RoleID == 2)
            {
                int ID = Membership.CurrentUser.Doctor.Id;
                Doctor doctor = Membership.CurrentUser.Doctor;
                //this.DoctorId = Membership.CurrentUser.Doctor.Id;
                //this.DoctorName =  Membership.CurrentUser.Doctor.Name;
                labelId.Content = Membership.CurrentUser.Doctor.Id;
                buttonLoadDoctor.IsEnabled = false;
                textBoxDoctorName.Text = Membership.CurrentUser.Doctor.Name;

            }

        }
        public EditVisit(int visitid)
            : this()
        {
            if (visitid == 0)
            {
                CreateNew();
            }
            else
            {
                Load(visitid);
                Flag = true;
            }

        }



        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            Save();
        }

        private void buttonLoadPatient_Click(object sender, RoutedEventArgs e)
        {
            Visit.Patient = PatientsDataAccess.GetPatientByName(textBoxPatientName.Text);
            if (Visit.Patient != null)
                MessageBox.Show("Пациент найден");
            else MessageBox.Show("Пациент не найден, попробуйте еще раз");
        }

        private void buttonLoadDoctor_Click(object sender, RoutedEventArgs e)
        {
            Visit.Doctor = DoctorDataAccess.SelectDoctorByName(textBoxDoctorName.Text);
            if (Visit.Patient != null)
                MessageBox.Show("Доктор найден");
            else MessageBox.Show("Доктор не найден, попробуйте еще раз");
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                Visit.file= Converter.ConvertImageToByteArray(selectedFileName);

            }
        }
        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            ShowPhoto showPhoto = new ShowPhoto(Visit.file);
            showPhoto.ShowDialog();
        }
    }
}
