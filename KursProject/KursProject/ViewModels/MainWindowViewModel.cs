using KursProject.Commands;
using KursProject.Commands.CommandForMainWindow;
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
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RegisterTrainerOpenCommand Register_Trainer_Click_Command;
        public RegisterTrainerOpenCommand Register_Trainer_Click
        {
            get
            {
                return Register_Trainer_Click_Command ??
                  (Register_Trainer_Click_Command = new RegisterTrainerOpenCommand(obj =>{}));
            }
        }


        private RegisterUserOpenCommand Register_Users_Click_Command;
        public RegisterUserOpenCommand Register_Users_Click
        {
            get
            {
                return Register_Users_Click_Command ??
                  (Register_Users_Click_Command = new RegisterUserOpenCommand(obj => {}));
            }
        }


        private InputToWorkWindowCommand Input_Click_Command;
        public InputToWorkWindowCommand Input_Click
        {
            get
            {
                return Input_Click_Command ??
                  (Input_Click_Command = new InputToWorkWindowCommand(obj => { }));
            }
        }

        private string[] Somebody = new string[2];
        public string[] Object
        {
            get { return Somebody; }
            set
            {
                Somebody = value;
                OnPropertyChanged("Object");
            }
        }
        public string Login
        {
            get { return Somebody[0]; }
            set
            {
                Somebody[0] = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return Somebody[1]; }
            set
            {
                Somebody[1] = value;
                OnPropertyChanged("Password");
            }
        }
    }
}
