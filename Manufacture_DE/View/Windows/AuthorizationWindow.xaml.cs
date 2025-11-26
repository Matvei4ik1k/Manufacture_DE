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
        int failedEntryCount = 0;
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
                        else
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                        }
                        Close();
                    }
                    
                    
                }
                else
                {
                    string login = App.context.SystemUser.FirstOrDefault(systemUser => systemUser.Login == LoginTb.Text).Login;

                    if (string.IsNullOrEmpty(login))
                    {
                        MessageBox.Show($"Введен неправильный логин и пароль.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        failedEntryCount++;
                        MessageBox.Show($"Введен неправильный пароль. Осталось попыток:{failedEntryCount} из 3", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                        if (failedEntryCount == 3)
                        {
                            MessageBox.Show("Пользователь заблокирован!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            failedEntryCount = 0;
                            SystemUser userToBlock = App.context.SystemUser.FirstOrDefault(systemUser => systemUser.Login == LoginTb.Text);
                            userToBlock.IsBlocked = true;
                            App.context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
