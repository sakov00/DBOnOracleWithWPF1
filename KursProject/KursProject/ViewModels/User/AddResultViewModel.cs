using KursProject.Commands;
using KursProject.Commands.CommandForEditUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class AddResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public AddResultViewModel()
        {
            client = WorkUserWindowViewModel.client;
        }
        private AddResultCommand Add_Result_Command;
        public AddResultCommand Add_Result_Click
        {
            get
            {
                return Add_Result_Command ??
                  (Add_Result_Command = new AddResultCommand(obj => { }));
            }
        }
        CLIENT client = new CLIENT();
        public string FirstName
        {
            get { return client.FIRSTNAME; }
            set
            {
                client.FIRSTNAME = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return client.SECONDNAME; }
            set
            {
                client.SECONDNAME = value;
                OnPropertyChanged("SecondName");
            }
        }
    }
}