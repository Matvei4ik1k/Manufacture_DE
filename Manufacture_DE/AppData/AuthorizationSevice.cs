using Manufacture_DE.Model;
using Manufacture_DE.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacture_DE.AppData
{
    class AuthorizationSevice
    {
        public static SystemUser CurrentUser { get; set; }

        public void SignIn(string login, string password)
        {
            CurrentUser = App.context.SystemUser.FirstOrDefault(user => user.Login == login&& user.Password==password);

            if (CurrentUser != null) { 
                if( CurrentUser.Role_id == 1)
                {
                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    administratorWindow.Show();
                }
                else
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                }
            }
        }
    }
}

