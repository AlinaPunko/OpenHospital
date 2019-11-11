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
            MessageBox.Show("Пиу");
            //var editConsultationForm = new EditConsultationForm(0);
            //editConsultationForm.ShowDialog();
            //this.Presenter.LoadConsultationsByCriterias();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
           
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
