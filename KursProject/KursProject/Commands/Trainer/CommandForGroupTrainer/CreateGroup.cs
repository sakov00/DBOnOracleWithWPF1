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
using System.Windows.Input;

namespace KursProject.Commands.CommandForGroupTrainer
{
    class CreateGroup : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public CreateGroup(Action<object> execute, Func<object, bool> canExecute = null)
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
                OracleCommand get_clients = new OracleCommand("GET_INFO_GROUPFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_clients.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_clients.Parameters.Add(pResult);
                get_clients.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                foreach (DataRow row1 in dt.Rows)
                    if (WorkTrainerWindowViewModel.trainer.ID_TRAINER == (decimal)row1[1])
                        throw new Exception("у тебя уже существует группа");

                    Random value = new Random();
                    decimal number = value.Next(0, 10000000);
                Found: foreach (DataRow row in dt.Rows)
                    {
                        if ((decimal)row[0] == number)
                        {
                            number = value.Next(0, 10000000);
                            goto Found;
                        }
                    }
                    OracleCommand insert_group = new OracleCommand("INSERT_GROUPFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    insert_group.CommandType = CommandType.StoredProcedure;
                    insert_group.Parameters.Add("NUMBER_GROUP", number);
                    insert_group.Parameters.Add("ID_TRAINER", WorkTrainerWindowViewModel.trainer.ID_TRAINER);
                    insert_group.ExecuteNonQuery();

                    OracleCommand update_trainer = new OracleCommand("EDIT_TRAINER_GROUP", (OracleConnection)WindowOfViews.database.Database.Connection);
                    update_trainer.CommandType = CommandType.StoredProcedure;
                    update_trainer.Parameters.Add("IDTRAINER1", WorkTrainerWindowViewModel.trainer.ID_TRAINER);
                    update_trainer.Parameters.Add("number_group1", number);
                    update_trainer.ExecuteNonQuery();
                    WorkTrainerWindowViewModel.trainer.NUMBER_GROUP = number;
                    //WindowOfViews.database.SaveChanges();
                    MessageBox.Show("группа создана, но в ней никто не участвует добавьте клиентов");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
