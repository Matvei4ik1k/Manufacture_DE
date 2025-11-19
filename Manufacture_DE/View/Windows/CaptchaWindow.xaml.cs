using Manufacture_DE.AppData;
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
using static System.Net.Mime.MediaTypeNames;

namespace Manufacture_DE.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        Captcha captcha;
        string[] imagesPath;
        public CaptchaWindow()
        {
            InitializeComponent();

            captcha = new Captcha();
            imagesPath = new string[]
            {

                @"C:\Users\user19\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\1.png",
                @"C:\Users\user19\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\2.png",
                @"C:\Users\user19\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\3.png",
                @"C:\Users\user19\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\4.png",
              
            };


            CaptchaFragmentsLb.ItemsSource = captcha.CreateFragments(imagesPath);
        }

        private void CaptchaFragmentsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CaptchaFragment captchaFragment = CaptchaFragmentsLb.SelectedItem as CaptchaFragment;

            CaptchaLb.Items.Add(captchaFragment);

            if (CaptchaLb.Items.Count >= 4)
            {
                if (captcha.IsCorrect(CaptchaLb.Items) == true)
                {
                    MessageBox.Show("Ура!");
                }
                else
                {
                    MessageBox.Show("Не Ура!");
                }
            }
        }

        private void CaptchaLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CaptchaFragment captchaFragment = CaptchaLb.SelectedItem as CaptchaFragment;

            CaptchaLb.Items.Remove(captchaFragment);
        }
    }
}
