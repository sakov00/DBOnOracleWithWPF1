using KursProject.Commands.CommandForEditUser;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class EditUserViewModel : INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            public EditUserViewModel()
            {
                trainer = WorkTrainerWindowViewModel.trainer;
            }

            private EditUserForExercises Edit_User_For_Exercises_Command;
            public EditUserForExercises Edit_User_For_Exercises_Click
            {
            get
            {
                return Edit_User_For_Exercises_Command ??
                  (Edit_User_For_Exercises_Command = new EditUserForExercises(obj => { }));
            }
            }

            private GetExercises Get_Exercises_Command;
            public GetExercises Get_Exercises_Click
        {
            get
            {
                return Get_Exercises_Command ??
                  (Get_Exercises_Command = new GetExercises(obj => { }));
            }
            }

            public static DataRowCollection GetClientsGroup()
            {
            OracleCommand get_clients = new OracleCommand("GET_INFO_CLIENT_FORGROUP", (OracleConnection)WindowOfViews.database.Database.Connection);
            get_clients.CommandType = CommandType.StoredProcedure;
            var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
            get_clients.Parameters.Add(pResult);
            get_clients.Parameters.Add("numbergroup", trainer.NUMBER_GROUP);
            get_clients.ExecuteNonQuery();
            var res = (OracleRefCursor)pResult.Value;
            OracleDataReader reader = res.GetDataReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.Rows;
            }
            private static TRAINER trainer = new TRAINER();
            public string FirstName
            {
                get { return trainer.FIRSTNAME; }
                set
                {
                    trainer.FIRSTNAME = value;
                    OnPropertyChanged("FirstName");
                }
            }
            public string SecondName
            {
                get { return trainer.SECONDNAME; }
                set
                {
                    trainer.SECONDNAME = value;
                    OnPropertyChanged("SecondName");
                }
            }
            public DataRowCollection SelectedListGroup
            {
            get { return GetClientsGroup(); }
            set
            {
                OnPropertyChanged("SelectedListGroup");
            }
            }
    }
}