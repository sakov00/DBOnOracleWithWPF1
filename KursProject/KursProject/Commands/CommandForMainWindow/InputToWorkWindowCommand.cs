using KursProject.Views;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForMainWindow
{
    class InputToWorkWindowCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public InputToWorkWindowCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
                        string[] text = (string[])parameter;
                        OracleCommand get_trainers = new OracleCommand("GET_INFO_TRAINER", (OracleConnection)WindowOfViews.database.Database.Connection);
                        get_trainers.CommandType = CommandType.StoredProcedure;
                        var pResult1 = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                        get_trainers.Parameters.Add(pResult1);
                        get_trainers.ExecuteNonQuery();
                        var res1 = (OracleRefCursor)pResult1.Value;
                        var trainers = res1.GetDataReader();

                        OracleCommand get_clients = new OracleCommand("GET_INFO_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                        get_clients.CommandType = CommandType.StoredProcedure;
                        var pResult2 = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                        get_clients.Parameters.Add(pResult2);
                        get_clients.ExecuteNonQuery();
                        var res2 = (OracleRefCursor)pResult2.Value;
                        var clients = res2.GetDataReader();

                        DataTable table_clients= new DataTable();
                        table_clients.Load(clients);
                        DataTable table_trainers = new DataTable();
                        table_trainers.Load(trainers);

                        bool Fine = false;
                        foreach (DataRow c in table_clients.Rows)
                        {
                            if (c[2].ToString().Replace(" ", "") == text[0] && c[3].ToString().Replace(" ", "") == text[1])
                            {
                                WindowOfViews.WorkUserWindow = new WorkUserWindow();
                                WindowOfViews.WorkUserWindow.Show();
                                Fine = true;
                            }
                        }
                        foreach (DataRow c in table_trainers.Rows)
                        {
                            if (c[4].ToString().Replace(" ", "") == text[0] && c[3].ToString().Replace(" ", "") == text[1])
                            {
                                WindowOfViews.WorkTrainerWindow = new WorkTrainerWindow();
                                WindowOfViews.WorkTrainerWindow.Show();
                                Fine = true;
                            }
                        }
                        if (Fine == true)
                        {
                            Application.Current.MainWindow.Close();
                        }
                        if (Fine == false)
                            MessageBox.Show("Вы неправильно ввели данные");
                        }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

}
