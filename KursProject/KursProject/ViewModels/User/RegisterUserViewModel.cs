using KursProject.Commands;
using KursProject.Commands.CommandForRegisterUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursProject.ViewModels
{
    class RegisterUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RegistrationUser Registration_User_Command;
        public RegistrationUser Registration_User_Click
        {
            get
            {
                return Registration_User_Command ??
                  (Registration_User_Command = new RegistrationUser(obj => {
                  }));
            }
        }
        public RegisterUserViewModel()
        {
            selectedclient.DATACLIENT = new List<DATACLIENT>();
            selectedclient.DATACLIENT.Add( new DATACLIENT());
        }

        private CLIENT selectedclient =new CLIENT();
        public CLIENT SelectedClient
        {
            get { return selectedclient; }
            set
            {
                selectedclient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public string FirstName
        {
            get { return selectedclient.FIRSTNAME; }
            set
            {
                selectedclient.FIRSTNAME = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return selectedclient.SECONDNAME; }
            set
            {
                selectedclient.SECONDNAME = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Login
        {
            get { return selectedclient.LOGIN; }
            set
            {
                selectedclient.LOGIN = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return selectedclient.PASSWORD; }
            set
            {
                selectedclient.PASSWORD = value;
                OnPropertyChanged("Password");
            }
        }
        public int Weight
        {
            get { return (int)selectedclient.DATACLIENT.LastOrDefault().WEIGHT; }
            set
            {
                selectedclient.DATACLIENT.LastOrDefault().WEIGHT = value;
                OnPropertyChanged("Weight");
            }
        }
        public int Height
        {
            get { return (int)selectedclient.DATACLIENT.LastOrDefault().HEIGHT; }
            set
            {
                selectedclient.DATACLIENT.LastOrDefault().HEIGHT = value;
                OnPropertyChanged("Height");
            }
        }
        public string Bodytype
        {
            get { return selectedclient.DATACLIENT.LastOrDefault().BODYTYPE; }
            set
            {
                selectedclient.DATACLIENT.LastOrDefault().BODYTYPE = value;
                OnPropertyChanged("BodyType");
            }
        }
    }
}
