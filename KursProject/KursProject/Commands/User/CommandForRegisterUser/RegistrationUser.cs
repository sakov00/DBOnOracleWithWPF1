using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForRegisterUser
{
    class RegistrationUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegistrationUser(Action<object> execute, Func<object, bool> canExecute = null)
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
                CLIENT client1 = (CLIENT)parameter;
                client1.DATACLIENT.LastOrDefault().ID_CLIENT= client1.ID_CLIENT;
                OracleCommand get_clients = new OracleCommand("GET_INFO_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_clients.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_clients.Parameters.Add(pResult);
                get_clients.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                var reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                OracleCommand get_dataclients = new OracleCommand("GET_INFO_DATACLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_dataclients.CommandType = CommandType.StoredProcedure;
                var pResult1 = new OracleParameter("pResult1", OracleDbType.RefCursor, ParameterDirection.Output);
                get_dataclients.Parameters.Add(pResult1);
                get_dataclients.ExecuteNonQuery();
                var res1 = (OracleRefCursor)pResult1.Value;
                var reader1 = res1.GetDataReader();
                int count = 0;
                while(reader1.Read())
                {
                    if (reader1[2].ToString() == client1.LOGIN)
                    {
                        throw new Exception("Такой логин уже используется");
                    }
                    count++;
                }
                    client1.ID_CLIENT = dt.Rows.Count + 1;
                    List<EXERCISESFORCLIENT> lol = GenerateExercises(parameter);
                    OracleCommand command1 = new OracleCommand("REG_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add("client_id", client1.ID_CLIENT);
                    command1.Parameters.Add("client_firstname", client1.FIRSTNAME);
                    command1.Parameters.Add("client_secondname", client1.SECONDNAME);
                    command1.Parameters.Add("client_login", client1.LOGIN);
                    command1.Parameters.Add("client_password", client1.PASSWORD);
                    command1.Parameters.Add("client_group", client1.NUMBER_GROUP);
                    command1.ExecuteNonQuery();
                    OracleCommand command2 = new OracleCommand("REG_CLIENT_DATA", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add("client_id_dataclient", count + 1);
                    command2.Parameters.Add("client_id_client", client1.ID_CLIENT);
                    command2.Parameters.Add("client_weight", client1.DATACLIENT.LastOrDefault().WEIGHT);
                    command2.Parameters.Add("client_height", client1.DATACLIENT.LastOrDefault().HEIGHT);
                    command2.Parameters.Add("client_bodytype", client1.DATACLIENT.LastOrDefault().BODYTYPE);
                    command2.Parameters.Add("client_progress", client1.DATACLIENT.LastOrDefault().PROGRESS_WEIGHT);
                    command2.Parameters.Add("client_progress", client1.DATACLIENT.LastOrDefault().PROGRESS_POWER);
                    command2.ExecuteNonQuery();
                for (int i = 0; i < lol.Count; i++)
                    {
                        OracleCommand command3 = new OracleCommand("INSERT_EXERCISESFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                        command3.CommandType = CommandType.StoredProcedure;
                        command3.Parameters.Add("ID_CLIENT", client1.ID_CLIENT);
                        command3.Parameters.Add("ID_EXERCISES", lol.ElementAt(i).ID_EXERCISES);
                        command3.Parameters.Add("EXERCISES", lol.ElementAt(i).EXERCISES);
                        command3.Parameters.Add("MUSCLEGROUPS", lol.ElementAt(i).MUSCLEGROUPS);
                        command3.Parameters.Add("DAYFOREXERCISE", lol.ElementAt(i).DAYFOREXERCISE);
                        command3.Parameters.Add("PRIORETY", lol.ElementAt(i).PRIORETY);
                        command3.ExecuteNonQuery();
                    }
                    //WindowOfViews.database.SaveChanges();
                    MessageBox.Show("регистрация прошла успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private List<EXERCISESFORCLIENT> GenerateExercises(object parameter)
        {
            List<EXERCISESFORCLIENT> list = new List<EXERCISESFORCLIENT>();
                try
                {
                    Random value = new Random();
                    int rand = value.Next(0, 8);
                    CLIENT client1 = (CLIENT)parameter;
                OracleCommand get_clients = new OracleCommand("GET_EXERCISESFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_clients.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_clients.Parameters.Add(pResult);
                get_clients.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                var reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                int number = dt.Rows.Count;
                    for (int k = 0; k < 18; k++)
                    {
                        list.Add(new EXERCISESFORCLIENT());
                        list.ElementAt(k).ID_EXERCISES = number + 1;
                        list.ElementAt(k).ID_CLIENT = client1.ID_CLIENT;
                    number++;
                    }
                    string First_Day = "";
                    string Second_Day = "";
                    string Third_Day = "";
                    OracleCommand get_exercises = new OracleCommand("GET_INFO_EXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_exercises.CommandType = CommandType.StoredProcedure;
                    var pResult1 = new OracleParameter("pResult1", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_exercises.Parameters.Add(pResult1);
                    get_exercises.ExecuteNonQuery();
                    var res1 = (OracleRefCursor)pResult1.Value;
                    OracleDataReader reader1 = res1.GetDataReader();
                    DataTable dt1 = new DataTable();
                    dt1.Load(reader1);
                    string[] GroupMus = Mas();
                    int i = 0;
                    int firstday = 0;
                    int secondday = 0;
                    int thirdday = 0;
                    if (rand >= 6)
                        rand = 6;
                    while (firstday < 6 || secondday < 6 || thirdday < 6)
                        foreach (DataRow c in dt1.Rows)
                        {
                            if (c.ItemArray[1].ToString() == GroupMus[rand])
                            {
                                First_Day += c.ItemArray[0].ToString() + ",";
                                firstday++;
                                if (firstday == 2 || firstday == 4 || firstday == 6)
                                    GroupMus[rand] = null;

                            }
                            if (c.ItemArray[1].ToString() == GroupMus[rand + 1])
                            {
                                Second_Day += c.ItemArray[0].ToString() + ",";
                                secondday++;
                                if (secondday == 2 || secondday == 4 || secondday == 6)
                                    GroupMus[rand + 1] = null;
                            }
                            if (c.ItemArray[1].ToString() == GroupMus[rand + 2])
                            {
                                Third_Day += c.ItemArray[0].ToString() + ",";
                                thirdday++;
                                if (thirdday == 2 || thirdday == 4 || thirdday == 6)
                                    GroupMus[rand + 2] = null;
                            }
                            if (GroupMus[rand] == null && GroupMus[rand + 1] == null && GroupMus[rand + 2] == null)
                            {
                                rand += 3;
                                if (rand >= 6)
                                    rand = 6;
                                if (GroupMus[8] == null && i == 0)
                                {
                                    i++;
                                    rand = 0;
                                }
                            }
                        }
                    
                    string[] first = First_Day.Split(new char[] { ',' });
                    string[] second = Second_Day.Split(new char[] { ',' });
                    string[] third = Third_Day.Split(new char[] { ',' });
                OracleCommand get_exercises2 = new OracleCommand("GET_INFO_EXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_exercises2.CommandType = CommandType.StoredProcedure;
                var pResult2 = new OracleParameter("pResult2", OracleDbType.RefCursor, ParameterDirection.Output);
                get_exercises2.Parameters.Add(pResult2);
                get_exercises2.ExecuteNonQuery();
                var res2 = (OracleRefCursor)pResult2.Value;
                OracleDataReader reader2 = res2.GetDataReader();
                DataTable dt2 = new DataTable();
                dt2.Load(reader2);
                int f = 0;
                int l = 0;
                int t = 0;
                while(f<18)
                    foreach(DataRow row in dt2.Rows)
                    {
                    for(int z=0;z<6;z++)
                    { 
                        if (f < 6 && row.ItemArray[0].ToString() == first[z])
                        {
                            list.ElementAt(f).EXERCISES = first[z];
                            list.ElementAt(f).MUSCLEGROUPS = row.ItemArray[1].ToString();
                            list.ElementAt(f).PRIORETY = (decimal)row.ItemArray[2];
                            list.ElementAt(f).DAYFOREXERCISE = "Понедельник";
                            f++;
                        }
                        else if (6<=f && f< 12 && row.ItemArray[0].ToString() == second[z])
                        {
                            list.ElementAt(f).EXERCISES = second[z];
                            list.ElementAt(f).MUSCLEGROUPS = row.ItemArray[1].ToString();
                            list.ElementAt(f).PRIORETY = (decimal)row.ItemArray[2];
                            list.ElementAt(f).DAYFOREXERCISE = "Среда";
                            f++;
                        }
                        else if (12 <= f && f < 18 && row.ItemArray[0].ToString() == third[z])
                        {
                            list.ElementAt(f).EXERCISES = third[z];
                            list.ElementAt(f).MUSCLEGROUPS = row.ItemArray[1].ToString();
                            list.ElementAt(f).PRIORETY = (decimal)row.ItemArray[2];
                            list.ElementAt(f).DAYFOREXERCISE = "Пятница";
                            f++;
                        }
                    }
                    }

                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
                   MessageBox.Show("не удалось сгенерировать программу тренировок");
                }
            return list;
        }

        private string[] Mas()
        {
            OracleCommand get_trainers = new OracleCommand("GET_INFO_EXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
            get_trainers.CommandType = CommandType.StoredProcedure;
            var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
            get_trainers.Parameters.Add(pResult);
            get_trainers.ExecuteNonQuery();
            var res = (OracleRefCursor)pResult.Value;
            var reader1 = res.GetDataReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            string[] GM = new string[9];
            int count = 0;
            foreach (DataRow c in dt1.Rows)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (GM[i] == c[1].ToString())
                        break;
                    if (c[1].ToString() != GM[count] && count == i)
                    {
                        GM[count] = c[1].ToString();
                        count++;
                        break;
                    }
                }
                if (count == 9)
                    break;
            }
            return GM;

        }
    }
}
