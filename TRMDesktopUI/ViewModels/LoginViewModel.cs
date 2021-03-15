using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

//Fix to passwordBox https://stackoverflow.com/questions/30631522/caliburn-micro-support-for-passwordbox

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        /// <summary>
        /// Checks if user provide correct data to perform login
        /// </summary>
        /// <returns></returns>
        public bool CanLogIn => UserName?.Length > 0 && Password?.Length > 0;

        public void LogIn()
        {
            MessageBox.Show("You logged", "TimCo Retail Manager");
        }
    }
}
