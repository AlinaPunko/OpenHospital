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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenHospital.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EditVisit.xaml
    /// </summary>
    public partial class EditVisit : UserControl
    {
        public Visit Visit { get; set; }
        bool Flag = false;

        
        protected void FillView()
        {
            if (Membership.CurrentUser.RoleID == 2)
            {
                int ID = Membership.CurrentUser.Doctor.Id;
                Doctor doctor = DoctorDataAccess.GetDoctorById(ID);
                Visit.Doctor = doctor;
                //View.DoctorId = doctor.DoctorID;
                textBoxDoctorName.Text = doctor.Name;
                //textBoxDoctorName.
            }
            else if (Membership.CurrentUser.RoleID == 3)
            {
                int ID = Membership.CurrentUser.Patient.Id;
                Patient patient = PatientsDataAccess.GetPatientById(ID);
                Visit.Patient = patient;
                textBoxDoctorName.Text = patient.Name;
            }
                else
            {
                int doctorId = Visit.Doctor!=null ? Visit.Doctor.Id : 1;
                //View.DoctorId = doctorId;
                var consultationDoctor = DoctorDataAccess.GetDoctorById(doctorId);
                if (consultationDoctor != null)
                {
                    textBoxDoctorName.Text = consultationDoctor.Name;
                }
                else
                {
                    textBoxDoctorName.Text = "Не выбран врач";
                }
            }

            int patientId = Visit.Patient != null ? Visit.Patient.Id : 1;
            //View.PatientId = patientId;
            var consultationPatient = PatientsDataAccess.GetPatientById(patientId);
            if (consultationPatient != null)
            {
                //View.PatientName = consultationPatient.Name;
            }
            else
            {
                //View.PatientName = "Не выбран пациент";
            }

            //DateTime scheduleDate = Visit.VisitDate.HasValue ? Visit.VisitDate.Value : DateTime.Now;
            //View.VisitDate = scheduleDate;

            //View.Notes = Visit.Notes;
            //View.Reason = Visit.Reason;
            //View.Prescription = Visit.Prescription;
            //View.VisitId = Visit.VisitID;
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

            if (!String.IsNullOrEmpty(dateTimePickerVisitDate.DisplayDate.ToString()))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Дата");
                isValid = false;
            }
            if (!String.IsNullOrEmpty(dateTimePickerVisitTime.ToString()))
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
                SaveModel(Visit);
                //FillView();
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
                //View.Message = message;
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

        public void Load(int visitId)
        {
            try
            {
                if (visitId == 0)
                {
                    throw new ArgumentNullException("visitId должен отличаться от 0!");
                }
                //var visit = VisitsDataAccess.GetVisit(visitId);
                //this.Visit = visit;
                this.FillView();
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
                //View.Message = message;
            }
        }
        public EditVisit()
        {
            List<string> data = new List<string>();
            data.Add("Первичный");
            data.Add("Вторичный");
            data.Add("Обследование");
            Type.ItemsSource = data;
            InitializeComponent();            
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
        public EditVisit(int visitId)
            : this()
        {
            if (visitId == 0)
            {
                //CreateNew();
            }
            else
            {
                //Load(visitId);
            }

        }

       

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //Save();
        }

        private void buttonLoadPatient_Click(object sender, RoutedEventArgs e)
        {
            //var patientsForm = new Patients();
            //Patient loadedPatient;
            //if (patientsForm.TryChoosePatient(out loadedPatient))
            //{
            //    this.PatientId = loadedPatient.PatientID;
            //    this.PatientName = loadedPatient.Name;
            //}
        }

        private void buttonLoadDoctor_Click(object sender, RoutedEventArgs e)
        {
            //var doctors = new Doctors();
            //Doctor loadedDoctor;
            //if (doctors.TryChooseDoctor(out loadedDoctor))
            //{
            //    this.DoctorId = loadedDoctor.DoctorID;
            //    this.DoctorName = loadedDoctor.Name;
            //}
        }
    }
}
