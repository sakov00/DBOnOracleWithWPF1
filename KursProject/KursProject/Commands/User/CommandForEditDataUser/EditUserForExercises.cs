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

namespace KursProject.Commands.CommandForEditUser
{
    class EditDataUserCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public EditDataUserCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
                OracleCommand get_clients = new OracleCommand("GET_INFO_DATACLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_clients.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_clients.Parameters.Add(pResult);
                get_clients.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }

                OracleCommand command = new OracleCommand("EDIT_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("FIRSTNAME1", WindowOfViews.EditDataUser.FirstName.Text);
                command.Parameters.Add("SECONDNAME1", WindowOfViews.EditDataUser.SecondName.Text);
                command.Parameters.Add("LOGIN1", WindowOfViews.EditDataUser.Login.Text);
                command.Parameters.Add("PASSWORD1", WindowOfViews.EditDataUser.Password.Text);
                command.Parameters.Add("client_id_dataclient", i+ 1);
                command.Parameters.Add("client_id_client", WorkUserWindowViewModel.client.ID_CLIENT);
                command.Parameters.Add("client_weight", int.Parse(WindowOfViews.EditDataUser.Weight.Text));
                command.Parameters.Add("client_height", int.Parse(WindowOfViews.EditDataUser.Height.Text));
                command.Parameters.Add("client_bodytype", WindowOfViews.EditDataUser.BodyType.Text);
                command.Parameters.Add("client_progress_power", "");
                command.ExecuteNonQuery();

                //db.Entry(WorkUserWindowViewModel.client).State = EntityState.Modified;
                //db.Entry(WorkUserWindowViewModel.client.DATACLIENT).State = EntityState.Modified;
                //WindowOfViews.database.SaveChanges();
                MessageBox.Show("изменения прошли успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
    }

