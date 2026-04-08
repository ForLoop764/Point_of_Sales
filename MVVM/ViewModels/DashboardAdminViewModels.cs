using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Point_of_Sales.MVVM.ViewModels
{
    public class DashboardAdminViewModels : INotifyPropertyChanged
    {
        private Random random = new Random();

        // 🔹 PROPERTIES
        private string sales;
        public string Sales
        {
            get => sales;
            set { sales = value; OnPropertyChanged(); }
        }

        private int transactions;
        public int Transactions
        {
            get => transactions;
            set { transactions = value; OnPropertyChanged(); }
        }

        private int activeUsers;
        public int ActiveUsers
        {
            get => activeUsers;
            set
            {
                activeUsers = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ActiveProgress));
            }
        }

        private string avgOrder;
        public string AvgOrder
        {
            get => avgOrder;
            set { avgOrder = value; OnPropertyChanged(); }
        }

        public double ActiveProgress => (double)ActiveUsers / 8;

        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<TransactionModel> RecentTransactions { get; set; }

        public DashboardAdminViewModels()
        {
            Sales = "₱48,230";
            Transactions = 184;
            ActiveUsers = 5;
            AvgOrder = "₱262";

            Users = new ObservableCollection<UserModel>
            {
                new UserModel { Name="Maria Santos", Role="Cashier", Status="Active", Color=Colors.Green },
                new UserModel { Name="Carlo Lim", Role="Cashier", Status="Active", Color=Colors.Green },
                new UserModel { Name="Ana Dela Cruz", Role="Supervisor", Status="Break", Color=Colors.Orange },
                new UserModel { Name="Rico Bautista", Role="Cashier", Status="Inactive", Color=Colors.Red }
            };

            RecentTransactions = new ObservableCollection<TransactionModel>();

            StartLiveUpdates(); //  START LIVE
        }

        // LIVE UPDATE METHOD
        private async void StartLiveUpdates()
        {
            while (true)
            {
                await Task.Delay(3000); // every 3 seconds

                // Update sales
                int addSale = random.Next(50, 500);
                int currentSales = int.Parse(Sales.Replace("₱", "").Replace(",", ""));
                currentSales += addSale;
                Sales = "₱" + currentSales.ToString("N0");

                // Update transactions
                Transactions += 1;

                // Random active users (3–8)
                ActiveUsers = random.Next(3, 9);

                // Add new transaction
                var newTx = new TransactionModel
                {
                    Id = "#" + random.Next(4000, 5000),
                    Amount = "₱" + random.Next(50, 500),
                    Type = GetRandomType(),
                    Color = GetColor()
                };

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RecentTransactions.Insert(0, newTx);

                    // limit to 10
                    if (RecentTransactions.Count > 10)
                        RecentTransactions.RemoveAt(10);
                });
            }
        }

        private string GetRandomType()
        {
            string[] types = { "Sale", "Refund", "Pending" };
            return types[random.Next(types.Length)];
        }

        private Color GetColor()
        {
            Color[] colors = { Colors.Green, Colors.Red, Colors.Orange };
            return colors[random.Next(colors.Length)];
        }

        // 🔹 MODELS
        public class UserModel
        {
            public string Name { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
            public Color Color { get; set; }
        }

        public class TransactionModel
        {
            public string Id { get; set; }
            public string Amount { get; set; }
            public string Type { get; set; }
            public Color Color { get; set; }
        }

        // 🔹 NOTIFY
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}