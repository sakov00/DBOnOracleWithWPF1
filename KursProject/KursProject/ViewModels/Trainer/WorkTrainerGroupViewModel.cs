using KursProject.Commands.CommandForGroupTrainer;
using KursProject.Commands.CommandForWorkTrainerWindow;
using KursProject.Views;
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
using System.Windows;

namespace KursProject.ViewModels
{
    class WorkTrainerGroupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkTrainerGroupViewModel()
        {
                trainer = WorkTrainerWindowViewModel.trainer;
        }

        private AddUser Add_User_For_Group_Command;
        public AddUser Add_User_For_Group_Click
        {
            get
            {
                return Add_User_For_Group_Command ??
                  (Add_User_For_Group_Command = new AddUser(obj => { }));
            }
        }

        private CreateGroup Create_Group_Command;
        public CreateGroup Create_Group_Click
        {
            get
            {
                return Create_Group_Command ??
                  (Create_Group_Command = new CreateGroup(obj => { }));
            }
        }
        public DataRowCollection SelectedList
        {
            get { return GetClients(); }
            set
            {
                OnPropertyChanged("SelectedList");
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
        public static DataRowCollection GetClients()
        {
            OracleCommand get_clients = new OracleCommand("GET_INFO_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
            get_clients.CommandType = CommandType.StoredProcedure;
            var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
            get_clients.Parameters.Add(pResult);
            get_clients.ExecuteNonQuery();
            var res = (OracleRefCursor)pResult.Value;
            OracleDataReader reader = res.GetDataReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.Rows;
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
    }
}
