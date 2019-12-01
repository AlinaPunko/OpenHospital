using OpenHospital.Data;
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
    /// Логика взаимодействия для ShowPhoto.xaml
    /// </summary>
    public partial class ShowPhoto : Window
    {

        public ShowPhoto()
        {
            InitializeComponent();
        }
        public ShowPhoto(byte[] arr)
        {
            InitializeComponent();
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage = Converter.ConvertByteArrayToImage(arr);
                result.ImageSource = bitmapImage;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
