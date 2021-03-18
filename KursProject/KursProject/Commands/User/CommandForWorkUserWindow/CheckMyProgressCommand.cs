using KursProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForWorkTrainerWindow
{
    class CheckMyProgressCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public CheckMyProgressCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        this.execute(parameter);
                        WindowOfViews.ChatTrainer = new ChatTrainer();
                        WindowOfViews.ChatTrainer.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
