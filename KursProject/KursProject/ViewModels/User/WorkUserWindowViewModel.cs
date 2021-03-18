using KursProject.Commands.CommandForWorkTrainerWindow;
using KursProject.Views;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursProject.ViewModels
{
    class WorkUserWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkUserWindowViewModel()
        {
            client = WindowOfViews.database.CLIENT.Where(i => i.LOGIN.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Login.Text &&
            i.PASSWORD.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Password.Text).FirstOrDefault();
        }

        private WorkGroupWithTrainer Work_Group_With_Trainer_Command;
        public WorkGroupWithTrainer Work_Group_With_Trainer_Click
        {
            get
            {
                return Work_Group_With_Trainer_Command ??
                  (Work_Group_With_Trainer_Command = new WorkGroupWithTrainer(obj => { }));
            }
        }

        private ChatWithTrainerCommand Chat_With_Trainer_Command;
        public ChatWithTrainerCommand Chat_With_Trainer_Click
        {
            get
            {
                return Chat_With_Trainer_Command ??
                  (Chat_With_Trainer_Command = new ChatWithTrainerCommand(obj => { }));
            }
        }

        private EditDataUserCommand Edit_Data_Client_Command;
        public EditDataUserCommand Edit_Data_Client_Click
        {
            get
            {
                return Edit_Data_Client_Command ??
                  (Edit_Data_Client_Command = new EditDataUserCommand(obj => { }));
            }
        }

        private CheckMyProgressCommand Check_My_Progress_Command;
        public CheckMyProgressCommand Check_My_Progress_Click
        { 
            get
            {
                return Check_My_Progress_Command ??
                  (Check_My_Progress_Command = new CheckMyProgressCommand(obj => { }));
            }
        }

        private AddResultCommandOpen Add_Result_Open_Command;
        public AddResultCommandOpen Add_Result_Open_Click
        {
            get
            {
                return Add_Result_Open_Command ??
                  (Add_Result_Open_Command = new AddResultCommandOpen(obj => { }));
            }
        }


        public static CLIENT client = new CLIENT();

        public CLIENT MyClient
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("MyClient");
            }
        }
        public string FirstName
        {
            get { return client.FIRSTNAME; }
            set
            {
                client.FIRSTNAME = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return client.SECONDNAME; }
            set
            {
                client.SECONDNAME = value;
                OnPropertyChanged("SecondName");
            }
        }
        //public double Progress
        //{
        //    get { return GetProgress(); }
        //    set
        //    {
        //        client.DATACLIENT.PROGRESS = (decimal?)value;
        //        OnPropertyChanged("Progress");
        //    }
        //}
        public DataRowCollection GetListExecises
        {
            get { return GetExercises();}
            set
            {
                OnPropertyChanged("GetListExecises");
            }
        }
        public static DataRowCollection GetExercises()
        {
                OracleCommand get_trainers = new OracleCommand("FIND_EXERCISES_FOR_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                get_trainers.CommandType = CommandType.StoredProcedure;
                var pResult = new OracleParameter("pResult", OracleDbType.RefCursor, ParameterDirection.Output);
                get_trainers.Parameters.Add(pResult);
                get_trainers.Parameters.Add("ident",client.ID_CLIENT);
                get_trainers.ExecuteNonQuery();
                var res = (OracleRefCursor)pResult.Value;
                OracleDataReader reader = res.GetDataReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
            return dt.Rows;        
        }

        //private static double GetProgress()
        //{
        //    double progress11 = 1;
        //    double progress12 = 1;
        //    double progress13 = 1;
        //    double progress21 = 1;
        //    double progress22 = 1;
        //    double progress23 = 1;
        //    int crossexer = 0;
        //    double progress = 0;

        //    var exer = WindowOfViews.database.RESULTEXERCISES.Where(i => i.ID_CLIENT == client.ID).ToList();
        //    if (exer.Count() != 0)
        //        try
        //        {

        //            var sortedexer = from u in exer
        //                             orderby u.DATETIME descending
        //                             select u;
        //            if (sortedexer.FirstOrDefault().FIRST_DAY_QUANTITY != null && sortedexer.FirstOrDefault().SECOND_DAY_QUANTITY != null && sortedexer.FirstOrDefault().THIRD_DAY_QUANTITY != null)
        //                crossexer = 6;
        //            if (sortedexer.FirstOrDefault().FIRST_DAY_QUANTITY == null || sortedexer.FirstOrDefault().SECOND_DAY_QUANTITY == null || sortedexer.FirstOrDefault().THIRD_DAY_QUANTITY == null)
        //                crossexer = 12;

        //            if (sortedexer.FirstOrDefault().FIRST_DAY_QUANTITY != null && sortedexer.FirstOrDefault().SECOND_DAY_QUANTITY == null && sortedexer.FirstOrDefault().THIRD_DAY_QUANTITY == null)
        //                crossexer += 6;
        //            if (sortedexer.FirstOrDefault().FIRST_DAY_QUANTITY == null && sortedexer.FirstOrDefault().SECOND_DAY_QUANTITY != null && sortedexer.FirstOrDefault().THIRD_DAY_QUANTITY == null)
        //                crossexer += 6;
        //            if (sortedexer.FirstOrDefault().FIRST_DAY_QUANTITY == null && sortedexer.FirstOrDefault().SECOND_DAY_QUANTITY == null && sortedexer.FirstOrDefault().THIRD_DAY_QUANTITY != null)
        //                crossexer += 6;

        //            for (int i = 0; i < crossexer; i++)
        //            {
        //                if (sortedexer.ElementAt(i).FIRST_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(i).FIRST_DAY_WEIGHT != null && sortedexer.ElementAt(i).FIRST_DAY_QUANTITY != null)
        //                    {
        //                        progress11 = int.Parse(sortedexer.ElementAt(i).FIRST_DAY_WEIGHT);
        //                        progress11 = progress11 * int.Parse(sortedexer.ElementAt(i).FIRST_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(i).FIRST_DAY_WEIGHT == null && sortedexer.ElementAt(i).FIRST_DAY_QUANTITY != null)
        //                        progress11 = int.Parse(sortedexer.ElementAt(i).FIRST_DAY_QUANTITY) * 10;
        //                }
        //                if (sortedexer.ElementAt(i).SECOND_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(i).SECOND_DAY_WEIGHT != null && sortedexer.ElementAt(i).SECOND_DAY_QUANTITY != null)
        //                    {
        //                        progress12 = int.Parse(sortedexer.ElementAt(i).SECOND_DAY_WEIGHT);
        //                        progress12 = progress12 * int.Parse(sortedexer.ElementAt(i).SECOND_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(i).SECOND_DAY_WEIGHT == null && sortedexer.ElementAt(i).SECOND_DAY_QUANTITY != null)
        //                        progress12 = int.Parse(sortedexer.ElementAt(i).SECOND_DAY_QUANTITY) * 10;
        //                }
        //                if (sortedexer.ElementAt(i).THIRD_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(i).THIRD_DAY_WEIGHT != null && sortedexer.ElementAt(i).THIRD_DAY_QUANTITY != null)
        //                    {
        //                        progress13 = int.Parse(sortedexer.ElementAt(i).THIRD_DAY_WEIGHT);
        //                        progress13 = progress13 * int.Parse(sortedexer.ElementAt(i).THIRD_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(i).THIRD_DAY_WEIGHT == null && sortedexer.ElementAt(i).THIRD_DAY_QUANTITY != null)
        //                        progress13 = int.Parse(sortedexer.ElementAt(i).THIRD_DAY_QUANTITY) * 10;
        //                }
        //            }
        //            //--------------------------------------------------------------------------------------------------------------------
        //            for (int k = sortedexer.Count() - crossexer; k < sortedexer.Count(); k++)
        //            {
        //                if (sortedexer.ElementAt(k).FIRST_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(k).FIRST_DAY_WEIGHT != null && sortedexer.ElementAt(k).FIRST_DAY_QUANTITY != null)
        //                    {
        //                        progress21 = int.Parse(sortedexer.ElementAt(k).FIRST_DAY_WEIGHT);
        //                        progress21 = progress21 * int.Parse(sortedexer.ElementAt(k).FIRST_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(k).FIRST_DAY_WEIGHT == null && sortedexer.ElementAt(k).FIRST_DAY_QUANTITY != null)
        //                        progress21 = int.Parse(sortedexer.ElementAt(k).FIRST_DAY_QUANTITY) * 10;
        //                }
        //                if (sortedexer.ElementAt(k).SECOND_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(k).SECOND_DAY_WEIGHT != null && sortedexer.ElementAt(k).SECOND_DAY_QUANTITY != null)
        //                    {
        //                        progress22 = int.Parse(sortedexer.ElementAt(k).SECOND_DAY_WEIGHT);
        //                        progress22 = progress22 * int.Parse(sortedexer.ElementAt(k).SECOND_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(k).SECOND_DAY_WEIGHT == null && sortedexer.ElementAt(k).SECOND_DAY_QUANTITY != null)
        //                        progress22 = int.Parse(sortedexer.ElementAt(k).SECOND_DAY_QUANTITY) * 10;
        //                }
        //                if (sortedexer.ElementAt(k).THIRD_DAY_QUANTITY != null)
        //                {
        //                    if (sortedexer.ElementAt(k).THIRD_DAY_WEIGHT != null && sortedexer.ElementAt(k).THIRD_DAY_QUANTITY != null)
        //                    {
        //                        progress23 = int.Parse(sortedexer.ElementAt(k).THIRD_DAY_WEIGHT);
        //                        progress23 = progress23 * int.Parse(sortedexer.ElementAt(k).THIRD_DAY_QUANTITY) * 0.8;
        //                    }
        //                    if (sortedexer.ElementAt(k).THIRD_DAY_WEIGHT == null && sortedexer.ElementAt(k).THIRD_DAY_QUANTITY != null)
        //                        progress23 = int.Parse(sortedexer.ElementAt(k).THIRD_DAY_QUANTITY) * 10;
        //                }
        //            }

        //            if (progress21 != 1 && progress22 != 1 && progress23 != 1)
        //                progress = (progress21 + progress22 + progress23) / (progress11 + progress12 + progress13);
        //            if (progress21 == 1)
        //                progress = (double)client.DATACLIENT.PROGRESS * (progress21 / progress11);
        //            if (progress22 == 1)
        //                progress = (double)client.DATACLIENT.PROGRESS * (progress22 / progress12);
        //            if (progress23 == 1)
        //                progress = (double)client.DATACLIENT.PROGRESS * (progress23 / progress13);
        //            client.DATACLIENT.PROGRESS = (decimal)Math.Round(progress, 2);
        //            WindowOfViews.database.Entry(client.DATACLIENT).State = EntityState.Modified;
        //            WindowOfViews.database.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    return Math.Round(progress, 2);
        //}

    }
    }
