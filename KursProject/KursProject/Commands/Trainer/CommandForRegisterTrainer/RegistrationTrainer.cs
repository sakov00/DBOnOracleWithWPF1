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

namespace KursProject.Commands.CommandForRegisterTrainer
{
    class RegistrationTrainer : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegistrationTrainer(Action<object> execute, Func<object, bool> canExecute = null)
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
                    Random value = new Random();
                    TRAINER trainer1 = (TRAINER)parameter;
                    OracleCommand get_trainers = new OracleCommand("GET_INFO_TRAINER", (OracleConnection)WindowOfViews.database.Database.Connection);
                    get_trainers.CommandType = CommandType.StoredProcedure;
                    var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                    get_trainers.Parameters.Add(pResult);
                    get_trainers.ExecuteNonQuery();
                    var res = (OracleRefCursor)pResult.Value;
                    var reader = res.GetDataReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    int rand = dt.Rows.Count+1;
                Found:
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[4].ToString() == trainer1.LOGIN)
                        {
                            throw new Exception("Такой логин уже используется");
                        }
                    }
                    if ((double)trainer1.DATATRAINER.BARBELLSQUAT < (double)trainer1.DATATRAINER.WEIGHT * 1.4 && (double)trainer1.DATATRAINER.DEADLIFT < (double)trainer1.DATATRAINER.WEIGHT * 1.8 &&
                        (double)trainer1.DATATRAINER.BENCHPRESS < (double)trainer1.DATATRAINER.WEIGHT * 1.2 && trainer1.DATATRAINER.PULLUPS < 10)
                        throw new Exception("Вы слабоваты для тренера");
                    OracleCommand command1 = new OracleCommand("REG_TRAINER", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.Add("trainer_id", rand);
                    command1.Parameters.Add("trainer_firstname", trainer1.FIRSTNAME);
                    command1.Parameters.Add("trainer_secondname", trainer1.SECONDNAME);
                    command1.Parameters.Add("trainer_login", trainer1.LOGIN);
                    command1.Parameters.Add("trainer_password", trainer1.PASSWORD);
                    command1.Parameters.Add("trainer_group", trainer1.NUMBER_GROUP);
                    command1.ExecuteNonQuery();
                    OracleCommand command2 = new OracleCommand("REG_TRAINER_DATA", (OracleConnection)WindowOfViews.database.Database.Connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add("trainer_id", rand);
                    command2.Parameters.Add("trainer_weight", trainer1.DATATRAINER.WEIGHT);
                    command2.Parameters.Add("trainer_height", trainer1.DATATRAINER.HEIGHT);
                    command2.Parameters.Add("trainer_BARBELLSQUAT", trainer1.DATATRAINER.BARBELLSQUAT);
                    command2.Parameters.Add("trainer_DEADLIFT", trainer1.DATATRAINER.DEADLIFT);
                    command2.Parameters.Add("trainer_BENCHPRESS", trainer1.DATATRAINER.BENCHPRESS);
                    command2.Parameters.Add("trainer_PULLUPS", trainer1.DATATRAINER.PULLUPS);
                    command2.Parameters.Add("trainer_BODYTYPE", trainer1.DATATRAINER.BODYTYPE);
                    command2.ExecuteNonQuery();
                    //WindowOfViews.database.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("значения введены неверно, перепроверьте пожалуйсто");

                }
        }
    }
}   
