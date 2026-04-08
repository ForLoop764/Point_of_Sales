using System;

namespace Point_of_Sales.MVVM.Models   // ← must match your folder
{
    public class Sale
    {
        public int IDMovie { get; set; }

        public string MovieName { get; set; }
        public double Price { get; set; }
        public DateTime SoldAt { get; set; } = DateTime.Now;
        public string FormattedTime => SoldAt.ToString("MMM dd, yyyy  hh:mm tt");
    }
}