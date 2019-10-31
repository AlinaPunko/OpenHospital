﻿using System;
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
    /// Логика взаимодействия для Doctors.xaml
    /// </summary>
    public partial class Doctors : UserControl
    {
        public Doctors()
        {
            InitializeComponent();
        }
        //public DoctorsPresenter Presenter { get; set; }
        private void DoctorsForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //    this.DragMove();
        }
        #region IDoctorsView Members

        //public IEnumerable<Data.Doctor> Doctors
        //{
        //    set
        //    {
        //        this.dataGridViewResult.AutoGenerateColumns = false;
        //        this.dataGridViewResult.DataContext = value;//datasource был
        //    }
        //}

        //public string Message
        //{
        //    set
        //    {

        //        Message message = new Message(value);
        //        message.Show();
        //    }
        //}

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


        #endregion

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            //var editDoctorForm = new EditDoctorForm(true);
            //editDoctorForm.ShowDialog();
            //this.Presenter.LoadDoctorsByCriterias();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            ////this.Presenter.LoadDoctorsByCriterias();
        }

        //private Doctor GetSelectedDoctor()
        //{
        //    //var row = this.dataGridViewResult.SelectedItem;//currentrow было вместо колумна
        //    //if (row == null)
        //    //{
        //    //    return null;
        //    //}

        //    //var doctor = (Doctor)row;//row.DataBoundItem;
        //    //return doctor;
        //}

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            //var selectedDoctor = this.GetSelectedDoctor();
            //if (selectedDoctor == null)
            //{
            //    return;
            //}

            //int selectedDoctorId = selectedDoctor.DoctorID;
            //var editDoctorForm = new EditDoctorForm(selectedDoctorId);
            //editDoctorForm.ShowDialog();
            //this.Presenter.LoadDoctorsByCriterias();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            //var selectedDoctor = this.GetSelectedDoctor();
            //if (selectedDoctor == null)
            //{
            //    return;
            //}

            //if (MessageBox.Show("Вы действительно хотите удалить этого врача?", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            //{
            //    return;
            //}

            //try
            //{
            //    int doctorId = selectedDoctor.DoctorID;
            //    UsersDataAccess.DeleteUserByDoctorId(doctorId);
            //    DoctorDataAccess.DeleteDoctorById(doctorId);
            //    this.Presenter.LoadAllDoctors();
            //}
            //catch (Exception ex)
            //{
            //    string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
            //    this.Message = errorMessage;
            //}
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {

            //this.DialogResult = DialogResult.HasValue;//.OK
            //this.Close();
        }

        public bool TryChooseDoctor(/*out Doctor doctor*/)
        {
            //doctor = null;
            //this.panelButtons.Visibility = Visibility.Hidden;//Visible = true;
            //this.panelChooseButtons.Visibility = Visibility.Visible; //.Visible = true;
            //this.ShowDialog();

            //if (this.DialogResult != DialogResult.Value)//.OK
            //{
            //    return false;
            //}

            //var selectedDoctor = GetSelectedDoctor();
            //if (selectedDoctor == null)
            //{
            //    return false;
            //}

            //doctor = selectedDoctor;

            return true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult.HasValue;//.Сancel
            //this.Close();
        }

        private void dataGridViewResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void textBoxSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.Presenter.LoadDoctorsByCriterias();
        }
    }
}
