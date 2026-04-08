using System.Collections.ObjectModel;
using System.Windows.Input;
using Point_of_Sales.MVVM.Models;

namespace Point_of_Sales.MVVM.ViewModels
{
    public class MoviePOSViewModel : BindableObject
    {
        // ✅ Shared instance across all pages
        public static MoviePOSViewModel Instance { get; } = new MoviePOSViewModel();

        public ObservableCollection<Movie> Movies { get; set; } = new();
        public ObservableCollection<Sale> Sales { get; set; } = new();

        public ICommand AddMovieCommand { get; }
        public ICommand SellTicketCommand { get; }

        public int TotalTicketsSold => Sales.Count;
        public double TotalRevenue => Sales.Sum(s => s.Price);

        private MoviePOSViewModel()
        {
            Movies.Add(new Movie { Name = "Avengers", TicketsAvailable = 50, Price = 250 });
            Movies.Add(new Movie { Name = "Spider-Man", TicketsAvailable = 30, Price = 200 });

            AddMovieCommand = new Command(async () => await AddMovie());
            SellTicketCommand = new Command<Movie>(SellTicket);
        }

        private async Task AddMovie()
        {
            string name = await Application.Current.MainPage.DisplayPromptAsync("Movie", "Enter movie name:");
            if (string.IsNullOrWhiteSpace(name)) return;

            string ticketsStr = await Application.Current.MainPage.DisplayPromptAsync("Tickets", "Enter number of tickets:");
            if (!int.TryParse(ticketsStr, out int tickets)) return;

            string priceStr = await Application.Current.MainPage.DisplayPromptAsync("Price", "Enter price:");
            if (!double.TryParse(priceStr, out double price)) return;

            Movies.Add(new Movie { Name = name, TicketsAvailable = tickets, Price = price });
        }

        private void SellTicket(Movie movie)
        {
            if (movie == null) return;

            if (movie.TicketsAvailable > 0)
            {
                movie.TicketsAvailable--;
                Sales.Add(new Sale { MovieName = movie.Name, Price = movie.Price });
                OnPropertyChanged(nameof(TotalTicketsSold));
                OnPropertyChanged(nameof(TotalRevenue));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Sold Out", $"{movie.Name} has no tickets left!", "OK");
            }
        }
    }
}