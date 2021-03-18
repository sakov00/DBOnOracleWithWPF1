using KursProject.Commands.CommandForRegisterTrainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class RegisterTrainerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RegistrationTrainer Registration_Trainer_Command;
        public RegistrationTrainer Registration_Trainer_Click
        {
            get
            {
                return Registration_Trainer_Command ??
                  (Registration_Trainer_Command = new RegistrationTrainer(obj => { }));
            }
        }
        private TRAINER selectedtrainer = new TRAINER();
        private DATATRAINER selecteddatatrainer = new DATATRAINER();
        public TRAINER SelectedTrainer
        {
            get { return selectedtrainer; }
            set
            {
                selectedtrainer = value;
                OnPropertyChanged("SelectedTrainer");
            }
        }
        public string FirstName
        {
            get { return selectedtrainer.FIRSTNAME; }
            set
            {
                selectedtrainer.FIRSTNAME = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return selectedtrainer.SECONDNAME; }
            set
            {
                selectedtrainer.SECONDNAME = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Login
        {
            get { return selectedtrainer.LOGIN; }
            set
            {
                selectedtrainer.LOGIN = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return selectedtrainer.PASSWORD; }
            set
            {
                selectedtrainer.PASSWORD = value;
                OnPropertyChanged("Password");
            }
        }
        public decimal Weight
        {
            get { return (int)selecteddatatrainer.WEIGHT; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.WEIGHT = value;
                OnPropertyChanged("Weight");
            }
        }
        public decimal Height
        {
            get { return (int)selecteddatatrainer.HEIGHT;}
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.HEIGHT = value;
                OnPropertyChanged("Height");
            }
        }
        public string BodyType
        {
            get { return selecteddatatrainer.BODYTYPE; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.BODYTYPE = value;
                OnPropertyChanged("BodyType");
            }
        }
        public decimal BarbellSquat
        {
            get { return (int)selecteddatatrainer.BARBELLSQUAT; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.BARBELLSQUAT = value;
                OnPropertyChanged("BarbellSquat");
            }
        }
        public decimal Deadlift
        {
            get { return (int)selecteddatatrainer.DEADLIFT; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.DEADLIFT = value;
                OnPropertyChanged("Deadlift");
            }
        }
        public decimal BenchPress
        {
            get { return (int)selecteddatatrainer.BENCHPRESS; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.BENCHPRESS = value;
                OnPropertyChanged("BenchPress");
            }
        }
        public decimal Pullups
        {
            get { return (int)selecteddatatrainer.PULLUPS; }
            set
            {
                selectedtrainer.DATATRAINER = selecteddatatrainer;
                selecteddatatrainer.PULLUPS = value;
                OnPropertyChanged("Pullups");
            }
        }
    }
}
