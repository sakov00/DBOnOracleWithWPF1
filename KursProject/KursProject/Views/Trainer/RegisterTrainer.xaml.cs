using KursProject.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace KursProject
{
    /// <summary>
    /// Логика взаимодействия для RegisterTrainer.xaml
    /// </summary>
    public partial class RegisterTrainer : Window
    {
        public RegisterTrainer()
        {
            InitializeComponent();
            DataContext = new RegisterTrainerViewModel();
        }
    }
}
