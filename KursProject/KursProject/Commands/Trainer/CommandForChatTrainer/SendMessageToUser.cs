using KursProject.ViewModels;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForEditUser
{
    class SendMessageToUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public SendMessageToUser(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public void Execute(object parameter)
        {
                try
                {
                    MESSAGES messages = new MESSAGES();
                    Random value = new Random();
                    OracleCommand get_trainer = new OracleCommand("GET_CLIENT_FOR_CHAT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_trainer.CommandType = CommandType.StoredProcedure;
                    var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_trainer.Parameters.Add(pResult);
                    get_trainer.Parameters.Add("LOGIN1", WindowOfViews.ChatTrainer.comboBox.Text);
                    get_trainer.ExecuteNonQuery();
                    var res = (OracleRefCursor)pResult.Value;
                    OracleDataReader reader = res.GetDataReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                OracleCommand get_messages = new OracleCommand("GET_INFO_MESSAGES", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_messages.CommandType = CommandType.StoredProcedure;
                var pResult1 = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_messages.Parameters.Add(pResult1);
                get_messages.ExecuteNonQuery();
                var res1 = (OracleRefCursor)pResult1.Value;
                OracleDataReader reader1 = res1.GetDataReader();
                DataTable dt1 = new DataTable();
                dt1.Load(reader1);

                OracleCommand command1 = new OracleCommand("INSERT_MESSAGE", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add("MESSAGES", WindowOfViews.ChatTrainer.messageText.Text);
                    command1.Parameters.Add("ID_MESSAGE", dt1.Rows.Count+1);
                    command1.Parameters.Add("WHO_SENDER", WorkTrainerWindowViewModel.trainer.ID_TRAINER);
                    command1.Parameters.Add("WHO_RECIPIENT", (decimal)dt.Rows[0][4]);
                    command1.Parameters.Add("GROUPMESSAGE", null);
                    command1.Parameters.Add("DATETIME", DateTime.Now);
                    command1.ExecuteNonQuery();

                    //WindowOfViews.database.SaveChanges();
                    ChatTrainerViewModel.lol.ChoiseClient = WindowOfViews.ChatTrainer.comboBox.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
