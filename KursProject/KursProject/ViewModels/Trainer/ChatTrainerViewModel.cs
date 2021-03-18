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
using System.Threading;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class ChatTrainerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ChatTrainerViewModel()
        {
            trainer = WorkTrainerWindowViewModel.trainer;
            lol = this;
            //Thread checkUserBlock = new Thread(new ThreadStart(ThreadFunc));
            //checkUserBlock.Start();
        }
        public static ChatTrainerViewModel lol;
        private SendMessageToUser Send_Message_Command;
        public SendMessageToUser Send_Message_Click
        {
            get
            {
                return Send_Message_Command ??
                  (Send_Message_Command = new SendMessageToUser(obj => { }));
            }
        }

        public static string[] GetClientsGroup()
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
            string[] mas = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                mas[i] = row[2].ToString();
                i++;
            }
            return mas;
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
        public string[] SelectedList
            {
            get { return GetClientsGroup();}
            set
            {
                OnPropertyChanged("SelectedList");
            }
            }
        private string Name;
        public string ChoiseClient
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged("ChoiseClient");
                ListMessage = GetMessages();
            }
        }
        private string Messages;
        public string ListMessage
        {
            get { return GetMessages();}
            set
            {
                Messages = value;
                OnPropertyChanged("ListMessage");
            }
        }
        //private void ThreadFunc()
        //{

        //    while (true)
        //    {
        //        Thread.Sleep(5000);
        //        GetMessages();
        //    }
        //}
         private string GetMessages()
        {
            if (WindowOfViews.ChatTrainer != null)
            {
                Messages = "";

                OracleCommand get_client = new OracleCommand("GET_CLIENT_FOR_CHAT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_client.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_client.Parameters.Add(pResult);
                get_client.Parameters.Add("LOGIN1", Name);
                get_client.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                DataTable datatableclient = new DataTable();
                datatableclient.Load(reader);


                OracleCommand get_messages = new OracleCommand("SORT_MESSAGES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_messages.CommandType = CommandType.StoredProcedure;
                var pResult1 = new OracleParameter("pResult1", OracleDbType.RefCursor, ParameterDirection.Output);
                get_messages.Parameters.Add(pResult1);
                get_messages.Parameters.Add("idrecipient", (decimal)datatableclient.Rows[0][4]);
                get_messages.Parameters.Add("idsender", WorkTrainerWindowViewModel.trainer.ID_TRAINER);
                get_messages.ExecuteNonQuery();
                var res1 = (OracleRefCursor)pResult1.Value;
                OracleDataReader reader1 = res1.GetDataReader();
                DataTable datatablemessages = new DataTable();
                datatablemessages.Load(reader1);

                    foreach(DataRow row in datatablemessages.Rows)
                    {
                        Messages += row[5] + " " + row[0] + Environment.NewLine;
                    }
                
            }
            return Messages;
        }
    }
}