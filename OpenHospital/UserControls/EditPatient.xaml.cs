using OpenHospital.Data;
using OpenHospital.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditPatient.xaml
    /// </summary>
    public partial class EditPatient
    {
        bool Flag = false;
        Patient patient = new Patient();
        public EditPatient()
        {
            InitializeComponent();
            if (Membership.CurrentUser.RoleID == 3)
            {
                //panel2.Visibility = Visibility.Collapsed;
                buttonClose.Visibility = Visibility.Collapsed;
                buttonSave.Visibility = Visibility.Collapsed;
               
            }
            if (Membership.CurrentUser.RoleID == 2)
            {
                
                buttonClose.Visibility = Visibility.Collapsed;
                buttonSave.Visibility = Visibility.Collapsed;

            }
            //this.Presenter = new EditPatientPresenter(this)
        }

        public EditPatient(int patientId) : this()
        {
            if (patientId == 0)
            {
                CreateNew();
            }
            else
            {
                Load(patientId);
                //LoadVisit();
            }
        }

        //private void buttonClose_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
        protected void LoadPatientById(int patientId)
        {
            //this.Presenter.Load(patientId);
        }



        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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
            string _regex = @"\d{12}";
            if (String.IsNullOrEmpty(textBoxName.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Имя");
                isValid = false;
            }
            if (dateTimePickerBirthdate.DisplayDate.Year < 1920 || dateTimePickerBirthdate.DisplayDate > DateTime.Now.Date)
            {
                message += String.Format("Поле '{0}' имеет неверный формат!\n", "Дата рождения");
                isValid = false;
            }

            ////if (String.IsNullOrEmpty(Patient.Id.ToString()))
            ////{
            ////    message += String.Format("Поле '{0}' пусто!\n", "Номер ");
            ////    isValid = false;
            ////}
            if (String.IsNullOrEmpty(textBoxAddress.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Адрес ");
                isValid = false;
            }
            if (!Regex.IsMatch(textBoxPhone.Text, _regex))
            {
                message += String.Format("Неверный формат телефона");
                isValid = false;
            }

            return isValid;
        }

        public void Save()
        {
            //this.FillPatient();
            bool isValid = IsValid();
            if (isValid)
            {
                if (Flag == false)
                {
                    //Patient patient = new Patient();
                    patient.Id = 0;
                    patient.Name = textBoxName.Text;
                    patient.Phone = textBoxPhone.Text;
                    patient.Address = textBoxAddress.Text;
                    patient.Birthdate = dateTimePickerBirthdate.DisplayDate;
                    ////Message message = new Message("Успешно");
                    //message.Show();
                    SaveModel(patient);
                }
                else
                {
                    patient.Name = textBoxName.Text;
                    patient.Phone = textBoxPhone.Text;
                    patient.Address = textBoxAddress.Text;
                    patient.Birthdate = dateTimePickerBirthdate.DisplayDate;
                    ////Message message = new Message("Успешно");
                    //message.Show();
                    SaveModel(patient);
                }
                //FillView();
            }
            else
            {
                MessageBox.Show("Проблема!");
            }
        }

        private void SaveModel(Patient model)
        {
            try
            {
                if (model.Id == 0)
                {
                    Patient patient = PatientsDataAccess.InsertPatient(model);
                    if (patient != null)
                    {
                        EditUser editUser = new EditUser(patient, true);
                        editUser.Show();


                    }
                    
                }
                else
                {
                    PatientsDataAccess.UpdatePatient(model);
                }
                //View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища");
                MessageBox.Show(message);
            }

        }

        public void CreateNew()
        {
            var newPatient = new Patient();
            //this.patient = newPatient;
            //this.FillView();
        }

        public void Load(int patientId)
        {
            try
            {
                if (patientId == 0)
                {
                    throw new ArgumentNullException("patientId должен отличаться от 0!");
                }
                patient = PatientsDataAccess.GetPatientById(patientId);
                textBoxAddress.Text = patient.Address;
                textBoxName.Text = patient.Name;
                textBoxPhone.Text = patient.Phone;
                dateTimePickerBirthdate.SelectedDate = patient.Birthdate;
                dataGridViewVisits.ItemsSource= VisitsDataAccess.GetVisitsByPatientId(patientId);
                Flag = true;
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
                MessageBox.Show(message);
            }
        }

        public void LoadVisit(int patientId)
        {
            try
            {
                //if (this.Patient == null || this.Patient.Id == 0)
                //{
                //    return;
                //}

                //int patientId = this.Patient.Id;
                var visits = VisitsDataAccess.GetVisitsByPatientId(patientId);
                Flag = true;
                //this.View.Visits = visits;
            }
            catch (Exception e)
            {
                string message = "Ошибка при загрузке диагнозов для пациента!\n" + e.Message;
                MessageBox.Show(message);
            }
        }
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var editVisitForm = new EditVisit();
            //editVisitForm.ShowDialog();
            //this.Presenter.LoadVisit();
            //this.Presenter.LoadConsultations();
        }

        private void dataGridViewConsultations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Text = "Имя " + textBoxName.Text.ToString() + "\r\nДата рождения " + dateTimePickerBirthdate.Text.ToString() + "\r\nТелефон "
                     + textBoxPhone.Text.ToString() + "\r\nАдрес " + textBoxAddress.Text.ToString() + "\r\n";
            Text += "ВИЗИТЫ\r\n";
                int count = dataGridViewVisits.Items.Count;
            for(int i =0; i<count; i++)
            {
                    var v = (System.Data.DataRowView)dataGridViewVisits.Items[i];
                Text += "Доктор " + v[1].ToString() + " дата и время " + v[2].ToString() + " тип " 
                    + v[3].ToString() + " симптомы " + v[4].ToString() + " диагноз " + v[5].ToString()
                    + " назначения " + v[6].ToString() + " пометки " + v[7].ToString() + " кабинет " + v[8].ToString() + "\r\n";
            }

            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(Text, new Font("Times New Roman", 12), new SolidBrush(System.Drawing.Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
                p.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occured While Printing " + ex.Message);
            }
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void DataGridViewVisits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
