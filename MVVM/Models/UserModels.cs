using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Point_of_Sales.MVVM.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(Initials)); }
        }

        private string _role;
        public string Role
        {
            get => _role;
            set { _role = value; OnPropertyChanged(); }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Initials =>
            string.Join("", Name.Split(' ').Select(x => x[0])).ToUpper();

        public string Color =>
            Status == "Active" ? "#43A047" :
            Status == "Inactive" ? "#E53935" :
            "#FF8C00";
    }
}