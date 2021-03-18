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
    class ReturnDataUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public ReturnDataUser(Action<object> execute, Func<object, bool> canExecute = null)
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
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                try
                {
                    OracleCommand get_client = new OracleCommand("FIND_INFO_CLIENT_FOR_ID", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_client.CommandType = CommandType.StoredProcedure;
                    var pResult1 = new OracleParameter("pResult1", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_client.Parameters.Add(pResult1);
                    get_client.Parameters.Add("id_client1", WorkUserWindowViewModel.client.ID_CLIENT);
                    get_client.ExecuteNonQuery();
                    var res1 = (OracleRefCursor)pResult1.Value;
                    OracleDataReader reader1 = res1.GetDataReader();
                    DataTable dt1 = new DataTable();
                    dt1.Load(reader1);

                    OracleCommand get_dataclient = new OracleCommand("FIND_INFO_DATACLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_dataclient.CommandType = CommandType.StoredProcedure;
                    var pResult2 = new OracleParameter("pResult2", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_dataclient.Parameters.Add(pResult2);
                    get_dataclient.Parameters.Add("Id_client1", WorkUserWindowViewModel.client.ID_CLIENT);
                    get_dataclient.ExecuteNonQuery();
                    var res2 = (OracleRefCursor)pResult2.Value;
                    OracleDataReader reader2 = res2.GetDataReader();
                    //DataTable dt2 = new DataTable();
                    //dt2.Load(reader2);
                    WindowOfViews.EditDataUser.FirstName.Text = dt1.Rows[0].ItemArray[0].ToString();
                    WindowOfViews.EditDataUser.SecondName.Text = dt1.Rows[0].ItemArray[1].ToString();
                    WindowOfViews.EditDataUser.Login.Text = dt1.Rows[0].ItemArray[2].ToString();
                    WindowOfViews.EditDataUser.Password.Text = dt1.Rows[0].ItemArray[3].ToString();
                    while (reader2.Read())
                    {
                        WindowOfViews.EditDataUser.Weight.Text = reader2[2].ToString();
                        WindowOfViews.EditDataUser.Height.Text = reader2[3].ToString();
                        WindowOfViews.EditDataUser.BodyType.Text = reader2[4].ToString();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
