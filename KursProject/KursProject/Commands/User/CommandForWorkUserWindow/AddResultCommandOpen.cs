using KursProject.ViewModels;
using KursProject.Views;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForWorkTrainerWindow
{
    class AddResultCommandOpen : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddResultCommandOpen(Action<object> execute, Func<object, bool> canExecute = null)
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
            WindowOfViews.AddResult = new AddResult();
            getExercises();
            WindowOfViews.AddResult.Show();
        }
        private static void getExercises()
        {
                try
                {
                
                    OracleCommand get_exercises = new OracleCommand("FIND_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_exercises.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_exercises.Parameters.Add(pResult);
                get_exercises.Parameters.Add("ident", WorkUserWindowViewModel.client.ID_CLIENT);
                get_exercises.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                
                    WindowOfViews.AddResult.FirstDayFirstExercise.Content = dt.Rows[0].ItemArray[2];
                    WindowOfViews.AddResult.FirstDaySecondExercise.Content = dt.Rows[1].ItemArray[2];
                    WindowOfViews.AddResult.FirstDayThirdExercise.Content = dt.Rows[2].ItemArray[2];
                    WindowOfViews.AddResult.FirstDayFourthExercise.Content = dt.Rows[3].ItemArray[2];
                    WindowOfViews.AddResult.FirstDayFifthExercise.Content = dt.Rows[4].ItemArray[2];
                    WindowOfViews.AddResult.FirstDaySixthExercise.Content = dt.Rows[5].ItemArray[2];

                WindowOfViews.AddResult.SecondDayFirstExercise.Content = dt.Rows[6].ItemArray[2];
                WindowOfViews.AddResult.SecondDaySecondExercise.Content = dt.Rows[7].ItemArray[2];
                WindowOfViews.AddResult.SecondDayThirdExercise.Content = dt.Rows[8].ItemArray[2];
                WindowOfViews.AddResult.SecondDayFourthExercise.Content = dt.Rows[9].ItemArray[2];
                WindowOfViews.AddResult.SecondDayFifthExercise.Content = dt.Rows[10].ItemArray[2];
                WindowOfViews.AddResult.SecondDaySixthExercise.Content = dt.Rows[11].ItemArray[2];

                WindowOfViews.AddResult.ThirdDayFirstExercise.Content = dt.Rows[12].ItemArray[2];
                WindowOfViews.AddResult.ThirdDaySecondExercise.Content = dt.Rows[13].ItemArray[2];
                WindowOfViews.AddResult.ThirdDayThirdExercise.Content = dt.Rows[14].ItemArray[2];
                WindowOfViews.AddResult.ThirdDayFourthExercise.Content = dt.Rows[15].ItemArray[2];
                WindowOfViews.AddResult.ThirdDayFifthExercise.Content = dt.Rows[16].ItemArray[2];
                WindowOfViews.AddResult.ThirdDaySixthExercise.Content = dt.Rows[17].ItemArray[2];

            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
