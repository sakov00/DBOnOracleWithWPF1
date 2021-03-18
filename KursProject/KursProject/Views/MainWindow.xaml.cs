using KursProject.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowOfViews.database.Database.Connection.Open();
            //XMLFunc();
            //Export_XML();
            InitializeComponent();
        }
        static void XMLFunc()
        {
            try
                {
                OracleCommand command1 = new OracleCommand("IMPORT_XML_MESSAGES", (OracleConnection)WindowOfViews.database.Database.Connection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.ExecuteNonQuery();

                OracleCommand command2 = new OracleCommand("IMPORT_XML_EXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.ExecuteNonQuery();

                OracleCommand command3 = new OracleCommand("IMPORT_XML_GROUPFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command3.CommandType = CommandType.StoredProcedure;
                command3.ExecuteNonQuery();

                OracleCommand command4 = new OracleCommand("IMPORT_XML_CLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command4.CommandType = CommandType.StoredProcedure;
                command4.ExecuteNonQuery();

                OracleCommand command5 = new OracleCommand("IMPORT_XML_TRAINER", (OracleConnection)WindowOfViews.database.Database.Connection);
                command5.CommandType = CommandType.StoredProcedure;
                command5.ExecuteNonQuery();

                OracleCommand command6 = new OracleCommand("IMPORT_XML_DATATRAINER", (OracleConnection)WindowOfViews.database.Database.Connection);
                command6.CommandType = CommandType.StoredProcedure;
                command6.ExecuteNonQuery();

                OracleCommand command7 = new OracleCommand("IMPORT_XML_DATACLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command7.CommandType = CommandType.StoredProcedure;
                command7.ExecuteNonQuery();

                OracleCommand command8 = new OracleCommand("IMPORT_XML_EXERCISESFORCLIENT", (OracleConnection)WindowOfViews.database.Database.Connection);
                command8.CommandType = CommandType.StoredProcedure;
                command8.ExecuteNonQuery();

                OracleCommand command9 = new OracleCommand("IMPORT_XML_RESULTEXERCISES", (OracleConnection)WindowOfViews.database.Database.Connection);
                command9.CommandType = CommandType.StoredProcedure;
                command9.ExecuteNonQuery();
                WindowOfViews.database.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static void Export_XML()
        {
            try
            {
                OracleCommand command1 = new OracleCommand("export_xml", (OracleConnection)WindowOfViews.database.Database.Connection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
