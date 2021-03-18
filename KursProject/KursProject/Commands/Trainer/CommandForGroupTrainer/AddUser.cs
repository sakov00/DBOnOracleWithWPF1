using KursProject.ViewModels;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForGroupTrainer
{
    class AddUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddUser(Action<object> execute, Func<object, bool> canExecute = null)
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
            this.execute(parameter);

                try
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
                    CLIENT client = new CLIENT();
                    TextBox text = (TextBox)parameter;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[4].ToString() == text.Text)
                        {
                            
                            OracleCommand edit_group_client = new OracleCommand("EDIT_GROUP_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                            edit_group_client.CommandType = CommandType.StoredProcedure;
                            edit_group_client.Parameters.Add("IDCLIENT1", int.Parse(text.Text));
                            edit_group_client.Parameters.Add("NUMBER_GROUP1", WorkTrainerWindowViewModel.trainer.NUMBER_GROUP);
                            edit_group_client.ExecuteNonQuery();
                        }
                    }
                    //WindowOfViews.database.SaveChanges();
                    MessageBox.Show($"клиент добавлен в группу под номером {text.Text}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
