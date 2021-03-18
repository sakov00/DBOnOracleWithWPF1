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
    class WorkTrainerWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkTrainerWindowViewModel()
        {

                trainer = WindowOfViews.database.TRAINER.Include("DATATRAINER").Include("GROUPFORCLIENT").Where(i => i.LOGIN.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Login.Text &&
                 i.PASSWORD.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Password.Text).FirstOrDefault();

        }
        private WorkGroupForTrainer Work_Group_Command;
        public WorkGroupForTrainer Work_Group_Click
        {
            get
            {
                return Work_Group_Command ??
                  (Work_Group_Command = new WorkGroupForTrainer(obj => {}));
            }
        }

        private ChatUserCommand Chat_User_Command;
        public ChatUserCommand Chat_User_Click
        {
            get
            {
                return Chat_User_Command ??
                  (Chat_User_Command = new ChatUserCommand(obj => { }));
            }
        }
        private EditUserCommand Edit_Client_Command;
        public EditUserCommand Edit_Client_Click
        {
            get
            {
                return Edit_Client_Command ??
                  (Edit_Client_Command = new EditUserCommand(obj => { }));
            }
        }
        public DataRowCollection SelectedProgress
        {
            get
            {
                OracleCommand get_clientsdata = new OracleCommand("GET_INFO_DATACLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_clientsdata.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_clientsdata.Parameters.Add(pResult);
                get_clientsdata.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt.Rows;
            }
            set
            {
                OnPropertyChanged("SelectedList");
            }
        }
        public DataRowCollection SelectedList
        {
            get
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
            set
            {
                OnPropertyChanged("SelectedList");
            }
        }
        public static TRAINER trainer = new TRAINER();

        public TRAINER MyTrainer
        {
            get { return trainer; }
            set
            {
                trainer = value;
                OnPropertyChanged("MyTrainer");
            }
        }
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
