using Manufacture_DE.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Manufacture_DE.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) ||
                string.IsNullOrEmpty(PasswordPb.Password))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                App.CurrentUser = App.context.SystemUser.FirstOrDefault(SystemUser => SystemUser.Login == LoginTb.Text && SystemUser.Login == LoginTb.Text && SystemUser.Password == PasswordPb.Password);
                if (App.CurrentUser != null)
                {
                    CaptchaWindow captchaWindow = new CaptchaWindow();
                    if (captchaWindow.ShowDialog() == true)
                    {
                        if (App.CurrentUser.Role_id == 1)
                        {
                            AdministratorWindow administrator = new AdministratorWindow();
                            administrator.Show();

                        }
                    }
                    else
                    {
                        UserWindow userWindow = new UserWindow();
                        userWindow.Show();
                    }

                    Close();
                }
                else
                {
                    //Блокировка
                }

            }
            
        }
    }
}
