using KursProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProject
{
    class WindowOfViews
    {
        public static WorkTrainerWindow WorkTrainerWindow;
        public static RegisterTrainer RegisterTrainer;
        public static GroupTrainer GroupTrainer;
        public static EditUser EditUser;
        public static ChatTrainer ChatTrainer;

        public static WorkUserWindow WorkUserWindow;
        public static EditDataUser EditDataUser;
        public static RegisterUser RegisterUser;
        public static ChatUser ChatUser;
        public static AddResult AddResult;

        public static BD_KURSACH_Entities database=new BD_KURSACH_Entities();
        
    }
}
