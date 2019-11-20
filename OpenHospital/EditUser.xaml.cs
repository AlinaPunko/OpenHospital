using OpenHospital.Data;
using OpenHospital.Model;
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
using System.Windows.Shapes;

namespace OpenHospital
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public User User { get; set; }
        public EditUser()
        {
            InitializeComponent();
            //this.Presenter = new EditUserPresenter(this);
        }
        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            if (!isValid)
            {
                labelMessage.Content = message;
            }
            return isValid;
        }

        protected bool IsDataValid(out string message)
        {
            message = string.Empty;
            bool isValid = true;

            if (String.IsNullOrEmpty(textBoxName.Text))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Имя пользователя");
                isValid = false;
            }

            if (String.IsNullOrEmpty(Password.Password))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Пароль");
                isValid = false;
            }

            if (Password.Password.Length < 3)
            {
                message += String.Format("Поле '{0}' должно быть длиннее 2!\n", "Пароль");
                isValid = false;
            }

            if (Password.Password != Password2.Password)
            {
                message += String.Format("Поля '{0}' и '{1}' не совпадают!\n", "Пароль", "Подтвердить пароль");
                isValid = false;
            }

            return isValid;
        }

        //public void Save()
        //{
        //    this.FillUser();
        //    bool isValid = IsValid();
        //    if (isValid)
        //    {
        //        SaveModel(User);
        //        FillView();
        //    }
        //    else
        //    {
        //        Message message = new Message("Проблема");
        //        message.Show();
        //    };
        //}

        private void SaveModel(User model)
        {
            try
            {
                if (User.ID == 0 )               
                    UsersDataAccess.InsertUser(User);
                else
                {
                    UsersDataAccess.UpdateUser(User);
                }

                labelMessage.Content = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища!Позвоните администратору!/ n {0} ",
                    e.Message);
                labelMessage.Content = message;
            }

        }

        public void CreateNew()
        {
            var newUser = new User() { RoleID = 1, Login = "", Doctor = null, Patient=null};
            this.User = newUser;
            //this.FillView();
        }
        public void CreateNew(Doctor doctor)
        {

            var newUser = new User() { RoleID = 2, Doctor = doctor, Patient=null, Login = "" };
            //var newDoctor = new Doctor() {/* Name = "Нет имени", Number = "Нет номера" */};
            //newUser.Doctor = newDoctor;
            this.User = newUser;
            //this.FillView();
        }
        public void CreateNew(Patient patient)
        {
            var newUser = new User() { RoleID = 3, Doctor = null, Patient = patient, Login = "" };
            //var newDoctor = new Doctor() {/* Name = "Нет имени", Number = "Нет номера" */};
            //newUser.Doctor = newDoctor;
            this.User = newUser;
            //this.FillView();
        }
        public void Load(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    throw new ArgumentNullException("userId должен отличаться от 0!");
                }

                var user = UsersDataAccess.GetUserById(userId);
                this.User = user;
                textBoxName.Text = User.Login;
                Password.Password = User.Password;
                Password2.Password = User.Password;
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
            }
        }
        public EditUser(int userId)
            : this()
        {
            InitializeComponent();
            if (userId == 0)
            {
               CreateNew();
            }
            else
            {
                Load(userId);
            }
        }
        public EditUser(Doctor doctor, bool flag) //: this()
        {
            InitializeComponent();
            CreateNew(doctor);
        }
        public EditUser(Patient patient, bool flag)// : this()
        {
            InitializeComponent();
            CreateNew(patient);
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
           
        {
            string message = "";
            if (IsDataValid(out message))
            {
                User.Login = textBoxName.Text;
                User.Password =Membership.GetHashString( Password.Password);
                SaveModel(User);
            }
        }
        private void EditUserForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //public EditUser()
        //{
        //    InitializeComponent();
        //}

        //public EditUser(object doctorID, bool v)
        //{
        //    this.doctorID = doctorID;
        //    this.v = v;
        //}
    }
}
