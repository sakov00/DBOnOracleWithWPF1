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

namespace KursProject.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupTrainer.xaml
    /// </summary>
    public partial class GroupTrainer : Window
    {
        public GroupTrainer()
        {
            InitializeComponent();
            DataContext = new WorkTrainerGroupViewModel();
        }
    }
}
