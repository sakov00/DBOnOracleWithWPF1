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
    class EditUserForExercises : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public EditUserForExercises(Action<object> execute, Func<object, bool> canExecute = null)
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
                    TextBox text = (TextBox)parameter;
                    int k = Int32.Parse(text.Text);
                    OracleCommand get_exercises = new OracleCommand("FIND_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_exercises.CommandType = CommandType.StoredProcedure;
                    var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_exercises.Parameters.Add(pResult);
                    get_exercises.Parameters.Add("ident", k);  
                    get_exercises.ExecuteNonQuery();
                    var res = (OracleRefCursor)pResult.Value;
                    OracleDataReader reader = res.GetDataReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    decimal[] mas = new decimal[20];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    mas[i] = (decimal)row[1];
                        i++;
                }
                        
                OracleCommand command1 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add("ID_CLIENT1", (decimal)k);
                    command1.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDayFirstExercise.Text);
                    command1.Parameters.Add("ID_EXERCISES1", mas[0]);
                    command1.ExecuteNonQuery();

                OracleCommand command2 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.Add("ID_CLIENT1", (decimal)k);
                command2.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDaySecondExercise.Text);
                command2.Parameters.Add("ID_EXERCISES1", mas[1]);
                command2.ExecuteNonQuery();

                OracleCommand command3 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command3.CommandType = CommandType.StoredProcedure;
                command3.Parameters.Add("ID_CLIENT1", (decimal)k);
                command3.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDayThirdExercise.Text);
                command3.Parameters.Add("ID_EXERCISES1", mas[2]);
                command3.ExecuteNonQuery();

                OracleCommand command4 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command4.CommandType = CommandType.StoredProcedure;
                command4.Parameters.Add("ID_CLIENT1", (decimal)k);
                command4.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDayFourthExercise.Text);
                command4.Parameters.Add("ID_EXERCISES1", mas[3]);
                command4.ExecuteNonQuery();

                OracleCommand command5 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command5.CommandType = CommandType.StoredProcedure;
                command5.Parameters.Add("ID_CLIENT1", (decimal)k);
                command5.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDayFifthExercise.Text);
                command5.Parameters.Add("ID_EXERCISES1", mas[4]);
                command5.ExecuteNonQuery();

                OracleCommand command6 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command6.CommandType = CommandType.StoredProcedure;
                command6.Parameters.Add("ID_CLIENT1", (decimal)k);
                command6.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.FirstDaySixthExercise.Text);
                command6.Parameters.Add("ID_EXERCISES1", mas[5]);
                command6.ExecuteNonQuery();

                OracleCommand command7 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command7.CommandType = CommandType.StoredProcedure;
                command7.Parameters.Add("ID_CLIENT1", (decimal)k);
                command7.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDayFirstExercise.Text);
                command7.Parameters.Add("ID_EXERCISES1", mas[6]);
                command7.ExecuteNonQuery();

                OracleCommand command8 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command8.CommandType = CommandType.StoredProcedure;
                command8.Parameters.Add("ID_CLIENT1", (decimal)k);
                command8.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDaySecondExercise.Text);
                command8.Parameters.Add("ID_EXERCISES1", mas[7]);
                command8.ExecuteNonQuery();

                OracleCommand command9 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command9.CommandType = CommandType.StoredProcedure;
                command9.Parameters.Add("ID_CLIENT1", (decimal)k);
                command9.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDayThirdExercise.Text);
                command9.Parameters.Add("ID_EXERCISES1", mas[8]);
                command9.ExecuteNonQuery();

                OracleCommand command10 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command10.CommandType = CommandType.StoredProcedure;
                command10.Parameters.Add("ID_CLIENT1", (decimal)k);
                command10.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDayFourthExercise.Text);
                command10.Parameters.Add("ID_EXERCISES1", mas[9]);
                command10.ExecuteNonQuery();

                OracleCommand command11 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command11.CommandType = CommandType.StoredProcedure;
                command11.Parameters.Add("ID_CLIENT1", (decimal)k);
                command11.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDayFifthExercise.Text);
                command11.Parameters.Add("ID_EXERCISES1", mas[10]);
                command11.ExecuteNonQuery();

                OracleCommand command12 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command12.CommandType = CommandType.StoredProcedure;
                command12.Parameters.Add("ID_CLIENT1", (decimal)k);
                command12.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.SecondDaySixthExercise.Text);
                command12.Parameters.Add("ID_EXERCISES1", mas[11]);
                command12.ExecuteNonQuery();

                OracleCommand command13 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command13.CommandType = CommandType.StoredProcedure;
                command13.Parameters.Add("ID_CLIENT1", (decimal)k);
                command13.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDayFirstExercise.Text);
                command13.Parameters.Add("ID_EXERCISES1", mas[12]);
                command13.ExecuteNonQuery();

                OracleCommand command14 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command14.CommandType = CommandType.StoredProcedure;
                command14.Parameters.Add("ID_CLIENT1", (decimal)k);
                command14.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDaySecondExercise.Text);
                command14.Parameters.Add("ID_EXERCISES1", mas[13]);
                command14.ExecuteNonQuery();

                OracleCommand command15 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command15.CommandType = CommandType.StoredProcedure;
                command15.Parameters.Add("ID_CLIENT1", (decimal)k);
                command15.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDayThirdExercise.Text);
                command15.Parameters.Add("ID_EXERCISES1", mas[14]);
                command15.ExecuteNonQuery();

                OracleCommand command16 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command16.CommandType = CommandType.StoredProcedure;
                command16.Parameters.Add("ID_CLIENT1", (decimal)k);
                command16.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDayFourthExercise.Text);
                command16.Parameters.Add("ID_EXERCISES1", mas[15]);
                command16.ExecuteNonQuery();

                OracleCommand command17 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command17.CommandType = CommandType.StoredProcedure;
                command17.Parameters.Add("ID_CLIENT1", (decimal)k);
                command17.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDayFifthExercise.Text);
                command17.Parameters.Add("ID_EXERCISES1", mas[16]);
                command17.ExecuteNonQuery();

                OracleCommand command18 = new OracleCommand("EDIT_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command18.CommandType = CommandType.StoredProcedure;
                command18.Parameters.Add("ID_CLIENT1", (decimal)k);
                command18.Parameters.Add("EXERCISES1", WindowOfViews.EditUser.ThirdDaySixthExercise.Text);
                command18.Parameters.Add("ID_EXERCISES1", mas[17]);
                command18.ExecuteNonQuery();
                // WindowOfViews.database.SaveChanges();
                MessageBox.Show("изменения прошли успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
