using System;
using System.Collections.Generic;

namespace Restaurant_Billing_System.DB_Prop
{
    public partial class LogEntry
    {
        public int LogId { get; set; }
        public int CustId { get; set; }
        public int DishId { get; set; }
        public int? Quantity { get; set; }
        public string DishName { get; set; } = null!;
        public int Price { get; set; }
        public double Value { get; set; }

        public virtual Customer Cust { get; set; } = null!;
        public virtual Dish Dish { get; set; } = null!;
    }
}
