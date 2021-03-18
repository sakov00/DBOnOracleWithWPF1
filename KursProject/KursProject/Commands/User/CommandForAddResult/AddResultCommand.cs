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

namespace KursProject.Commands
{
    class AddResultCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddResultCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
                List<RESULTEXERCISES> listresult = new List<RESULTEXERCISES>();
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

                OracleCommand get_workout = new OracleCommand("GET_INFO_WORKOUT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_workout.CommandType = CommandType.StoredProcedure;
                var pResult1 = new OracleParameter("pResult1", OracleDbType.RefCursor, ParameterDirection.Output);
                get_workout.Parameters.Add(pResult1);
                get_workout.ExecuteNonQuery();
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
                decimal id = 0;
                while (reader2.Read())
                {
                    id = (decimal)reader2[0];
                }
                OracleCommand command = new OracleCommand("INSERT_WORKOUT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("NUMBERWORKOUT", dt1.Rows.Count + 1);
                command.Parameters.Add("ID_DATACLIENT", id);
                command.Parameters.Add("PULSE", int.Parse(WindowOfViews.AddResult.Pulse.Text));
                command.Parameters.Add("DIFFICULTY_WORKOUT", WindowOfViews.AddResult.Difficulty_Workout.Text);
                command.Parameters.Add("INTENSITY", WindowOfViews.AddResult.Intencity.Text);
                command.Parameters.Add("COUNTEXERCISES", 6);
                command.ExecuteNonQuery();
                OracleCommand get_resultexercises = new OracleCommand("GET_INFO_RESULTEXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_resultexercises.CommandType = CommandType.StoredProcedure;
                var pResult4 = new OracleParameter("pResult4", OracleDbType.RefCursor, ParameterDirection.Output);
                get_resultexercises.Parameters.Add(pResult4);
                get_resultexercises.ExecuteNonQuery();
                var res4 = (OracleRefCursor)pResult4.Value;
                OracleDataReader reader4 = res4.GetDataReader();
                DataTable dt4 = new DataTable();
                dt4.Load(reader4);


                int num = 0;
                Random value = new Random();
                int count = dt4.Rows.Count+1;
                foreach (DataRow c in dt.Rows)
                {
                    listresult.Add(new RESULTEXERCISES());
                    listresult.ElementAt(num).NUMBERWORKOUT = dt1.Rows.Count + 1;
                    listresult.ElementAt(num).ID_EXERCISES = count;
                    listresult.ElementAt(num).EXERCISES = c[2].ToString();
                    listresult.ElementAt(num).DAYFOREXERCISE = c[4].ToString();
                    listresult.ElementAt(num).MUSCLEGROUPS = c[3].ToString();
                    listresult.ElementAt(num).PRIORETY = (decimal)c[5];
                    count++;
                    num++;
                }
                num = 0;
                {
                    if (WindowOfViews.AddResult.FirstDay1_Copy.Text != "" && WindowOfViews.AddResult.FirstDay1.Text != "")
                    {
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay1_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay1.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay2_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay2.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay3_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay3.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay4_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay4.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay5_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay5.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.FirstDay6_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.FirstDay6.Text);
                        num++;
                    }
                    if (WindowOfViews.AddResult.SecondDay1_Copy.Text != "" && WindowOfViews.AddResult.SecondDay1.Text != "")
                    {
                        num = 6;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay1_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay1.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay2_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay2.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay3_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay3.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay4_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay4.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay5_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay5.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.SecondDay6_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.SecondDay6.Text);
                        num++;
                    }
                    if (WindowOfViews.AddResult.ThirdDay1_Copy.Text != "" && WindowOfViews.AddResult.ThirdDay1.Text != "")
                    {
                        num = 12;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay1_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay1.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay2_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay2.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay3_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay3.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay4_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay4.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay5_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay5.Text);
                        num++;
                        listresult.ElementAt(num).WEIGHT_FOR_EXERCISES = int.Parse(WindowOfViews.AddResult.ThirdDay6_Copy.Text);
                        listresult.ElementAt(num).QQUANTITY = int.Parse(WindowOfViews.AddResult.ThirdDay6.Text);
                        num++;
                    }
                }

                bool fics = false;
                    int k;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (listresult.ElementAt(i).WEIGHT_FOR_EXERCISES.ToString() != "" && !int.TryParse(listresult.ElementAt(i).WEIGHT_FOR_EXERCISES.ToString(), out k))
                            fics = true;
                        if (listresult.ElementAt(i).QQUANTITY.ToString() != "" && !int.TryParse(listresult.ElementAt(i).QQUANTITY.ToString(), out k))
                            fics = true;
                    }
                    if (fics==true)
                    {
                        throw new Exception("неверно введено значение");
                    }
                    if (fics==false)
                    {
                    int number= listresult.Count;
                    int i = 0;
                    if (listresult.ElementAt(0).WEIGHT_FOR_EXERCISES != null && listresult.ElementAt(0).QQUANTITY != null)
                    {
                        i = 0;
                        number = 6;
                    }
                    if (listresult.ElementAt(6).WEIGHT_FOR_EXERCISES != null && listresult.ElementAt(6).QQUANTITY != null)
                    {
                        i = 6;
                        number = 12;
                    }
                    if (listresult.ElementAt(12).WEIGHT_FOR_EXERCISES != null && listresult.ElementAt(12).QQUANTITY != null)
                    {
                        i = 12;
                        number = 18;
                    }
                    for (i=i; i < number; i++)
                    {
                            OracleCommand command5 = new OracleCommand("INSERT_RESULT_EXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                            command5.CommandType = CommandType.StoredProcedure;
                            command5.Parameters.Add("NUMBERWORKOUT", listresult.ElementAt(i).NUMBERWORKOUT);
                            command5.Parameters.Add("ID_EXERCISES", listresult.ElementAt(i).ID_EXERCISES);
                            command5.Parameters.Add("EXERCISES", listresult.ElementAt(i).EXERCISES);
                            command5.Parameters.Add("WEIGHT_FOR_EXERCISES", listresult.ElementAt(i).WEIGHT_FOR_EXERCISES);
                            command5.Parameters.Add("QQUANTITY", listresult.ElementAt(i).QQUANTITY);
                            command5.Parameters.Add("DAYFOREXERCISE", listresult.ElementAt(i).DAYFOREXERCISE);
                            command5.Parameters.Add("MUSCLEGROUPS", listresult.ElementAt(i).MUSCLEGROUPS);
                            command5.Parameters.Add("PRIORETY", listresult.ElementAt(i).PRIORETY);
                            command5.ExecuteNonQuery();
                    }
                    //WindowOfViews.database.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
