
using OpenHospital.Data;
using OpenHospital.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace OpenHospital
{
    /// <summary>
    /// Логика взаимодействия для EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Window//, IEditDoctorView 
    {
        public Doctor Doctor { get; set; }



        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            //View.Message = message;
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
            if (String.IsNullOrEmpty(textBoxAddress.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Адрес");
                isValid = false;
            }
            //if (String.IsNullOrEmpty(Doctor.Skils))
            //{
            //    message += String.Format("Поле '{0}' пусто!\n", "Опыт");
            //    isValid = false;
            //}
            if (!Regex.IsMatch(textBoxPhone.Text, _regex))
            {
                message += String.Format("Неверный формат телефона");
                isValid = false;
            }
            Doctor.Name = textBoxName.Text;
            Doctor.Phone = textBoxPhone.Text;
            Doctor.Address = textBoxAddress.Text;
            Category category = new Category(DoctorDataAccess.SelectCatIdByCat(textBoxCat.SelectedValue.ToString()), textBoxCat.SelectedValue.ToString());
            Specialization specialization = new Specialization(DoctorDataAccess.SelectSpecIdByName(textBoxSpec.Text), textBoxSpec.Text);
            Doctor.Category1 = category;
            Doctor.Specialization1 = specialization;

            return isValid;
        }

        public void Save()
        {

            //this.FillDoctor();
            bool isValid = IsValid();
            if (isValid)
            {
                //Message message = new Message("Успешно");
                //message.Show();
                SaveModel(Doctor);
               // FillView();
            }
            else
            {

//Message message = new Message("Проблема");
                //message.Show();
                // FillView();
            }
        }
        public void Save1()
        {

            //this.FillDoctor();
            bool isValid = IsValid();
            if (isValid)
            {
                //Message message = new Message("Успешно");
                SaveModel(Doctor);
                //FillView();
                EditUser editUserForm = new EditUser(Doctor, true);
                editUserForm.Show();
            }

            else
            {
                ////Message message = new Message("Проблема");
                ////message.Show();
            }
        }

        private void SaveModel(Doctor model)
        {
            try
            {
                if (Doctor.Id == 0)
                {
                    DoctorDataAccess.InsertDoctor(Doctor);
                }
                else
                {
                    DoctorDataAccess.UpdateDoctor(Doctor);
                }
                //View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища!Позвоните администратору!/ n [0] ", e.Message);
                //View.Message = message;
            }

        }

        public void CreateNew()
        {
            var newDoctor = new Doctor();
            this.Doctor = newDoctor;
            //this.FillView();
        }

        public void CreateNew(bool flag)
        {
            var newDoctor = new Doctor();
            this.Doctor = newDoctor;
            //this.FillView();
        }

        public void Load(Doctor doctor)
        {
            try
            {
                if (doctor == null)
                {
                    throw new ArgumentNullException("doctorId должен отличаться от 0!");
                }
                //var doctor = DoctorDataAccess.GetDoctorById(doctorId);
                this.Doctor = doctor;
                textBoxAddress.Text = Doctor.Address;
                textBoxName.Text = Doctor.Name;
                textBoxPhone.Text = Doctor.Phone;
               // this.FillView();
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
              //  View.Message = message;
            }
        }
        public bool Flag = false;
        public EditDoctor()
        {
            InitializeComponent();
            List<string> text = new List<string> { "Первая","Вторая","Высшая" };
            textBoxCat.ItemsSource = text;
            //this.Presenter = new EditDoctorPresenter(this);         

        }
        //public EditDoctorPresenter Presenter { get; set; }

        public EditDoctor(Doctor doctor) : this()
        {
            if (doctor==null)
            {
                CreateNew();
            }
            else
            {
                Load(doctor);
            }
        }
        public EditDoctor(bool flag) : this()
        {
            Flag = true;
            CreateNew(true);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Flag == false)
                Save();
            else
                Save1();
            this.Close();
        }
        protected void LoadDoctorById(int doctorId)
        {
            //Load(doctorId);
        }
        private void EditDoctorForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
