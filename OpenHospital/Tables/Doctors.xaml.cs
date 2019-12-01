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
using OpenHospital.Data;
using OpenHospital.Model;

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
            List<string> Cat = new List<string>();
            Cat.Add("");
            Cat.Add("Первая");
            Cat.Add("Вторая");
            Cat.Add("Высшая");
            Category.ItemsSource = Cat;
            SelectAllDoctors();
            if(Membership.CurrentUser.RoleID!=1)
            panelButtons.Visibility = Visibility.Collapsed;
            //dataGridViewResult.AutoGenerateColumns = false;
            //dataGridViewResult.Columns[1].Header.= "Name";
                
        }

        private void SelectAllDoctors()
        {
            
            dataGridViewResult.ItemsSource= DoctorDataAccess.GetDoctors();
            //throw new NotImplementedException();
        }

        private void DoctorsForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //    this.DragMove();
        }


        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var editDoctor = new EditDoctor(true);
            editDoctor.ShowDialog();
            //this.Presenter.LoadDoctorsByCriterias();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Category.SelectedItem != null && Category.SelectedItem.ToString()!="")
                {

                    dataGridViewResult.ItemsSource = DoctorDataAccess.GetDoctorsByCat(Category.SelectedItem.ToString());
                    return;
                }
                if (!String.IsNullOrEmpty(Spec.Text))
                {
                    dataGridViewResult.ItemsSource = DoctorDataAccess.GetDoctorsBySpec(Spec.Text);
                    return;
                }
                if(Category.SelectedItem.ToString()=="" && String.IsNullOrEmpty(Spec.Text))
                    dataGridViewResult.ItemsSource = DoctorDataAccess.GetDoctors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
            var editDoctor = new EditDoctor(DoctorDataAccess.GetDoctorById(Convert.ToInt32(row.Row.ItemArray[0].ToString())));
            editDoctor.ShowDialog();
            //this.Presenter.LoadDoctorsByCriterias();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = (System.Data.DataRowView)dataGridViewResult.SelectedItems[0];
            if (row == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить этого доктора ? ", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                //var patient = (Patient)row;
                //int patientId = patient.Id;
                UsersDataAccess.DeleteUserByDoctorId(Convert.ToInt32(row.Row.ItemArray[0].ToString()));
                SelectAllDoctors();
                //DoctorDataAccess.DeleteDoctorById(Convert.ToInt32(row.Row.ItemArray[0].ToString()));


            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                MessageBox.Show(errorMessage);
            }
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

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
