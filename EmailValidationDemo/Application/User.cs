using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UserAutorizationDemo.Application
{
    public class User : INotifyPropertyChanged
    {

        private string _password;
        private string _email;

        [RegexPattern(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", Error = "Please enter a valid email address")]
        public string Email
        {
            get { return _email ; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        [RegexPattern(@"(([A-z0-9]{3,})([0-9]+)([A-z]+)?)", Error = "Password must contain only letters and numbers.")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
