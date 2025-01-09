using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1.Modell
{
    public class Authors : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                Fio = Fio;
                OnPropertyChanged("Surname");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Fio = Fio;
                OnPropertyChanged("Name");
            }
        }
        private string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                Fio = Fio;
                OnPropertyChanged("Lastname");
            }
        }
        private string _fio;
        public string Fio
        {
            get { return _fio; }
            set
            {
                _fio = $"{_surname} {_name} {_lastname}";
                OnPropertyChanged("Fio");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
