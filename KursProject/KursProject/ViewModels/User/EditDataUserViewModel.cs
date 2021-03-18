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
    class EditDataUserViewModel : INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            private EditDataUserCommand Edit_User_For_Exercises_Command;
            public EditDataUserCommand Edit_User_For_Exercises_Click
            {
            get
            {
                return Edit_User_For_Exercises_Command ??
                  (Edit_User_For_Exercises_Command = new EditDataUserCommand(obj => { }));
            }
            }
            private ReturnDataUser Return_Data_User_Command;
            public ReturnDataUser Return_Data_User_Click
            {
            get
            {
                return Return_Data_User_Command ??
                  (Return_Data_User_Command = new ReturnDataUser(obj => { }));
            }
            }
    }
}