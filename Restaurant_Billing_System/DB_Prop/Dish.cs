using System;
using System.Collections.Generic;

namespace Restaurant_Billing_System.DB_Prop
{
    public partial class Dish
    {
        public Dish()
        {
            LogEntries = new HashSet<LogEntry>();
        }

        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public int Price { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }
    }
}
