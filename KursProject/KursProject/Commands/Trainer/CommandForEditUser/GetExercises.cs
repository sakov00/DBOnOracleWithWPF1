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
    class GetExercises : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public GetExercises(Action<object> execute, Func<object, bool> canExecute = null)
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
                    int num = 0;
                    TextBox text = (TextBox)parameter;
                    decimal k = decimal.Parse(text.Text);
                    OracleCommand get_trainers = new OracleCommand("FIND_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_trainers.CommandType = CommandType.StoredProcedure;
                    var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_trainers.Parameters.Add(pResult);
                    get_trainers.Parameters.Add("ident", k);
                    get_trainers.ExecuteNonQuery();
                    var res = (OracleRefCursor)pResult.Value;
                    OracleDataReader reader = res.GetDataReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    WindowOfViews.EditUser.FirstDayFirstExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.SecondDayFirstExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.ThirdDayFirstExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.FirstDaySecondExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.SecondDaySecondExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.ThirdDaySecondExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.FirstDayThirdExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.SecondDayThirdExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.ThirdDayThirdExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.FirstDayFourthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.SecondDayFourthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.ThirdDayFourthExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.FirstDayFifthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.SecondDayFifthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.ThirdDayFifthExercise.Text = (string)dt.Rows[num][2];
                    num++;
                    WindowOfViews.EditUser.FirstDaySixthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.SecondDaySixthExercise.Text = (string)dt.Rows[num][2];
                num++;
                WindowOfViews.EditUser.ThirdDaySixthExercise.Text = (string)dt.Rows[num][2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
