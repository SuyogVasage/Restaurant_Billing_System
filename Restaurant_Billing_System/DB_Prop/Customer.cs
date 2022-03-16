using System;
using System.Collections.Generic;

namespace Restaurant_Billing_System.DB_Prop
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
            LogEntries = new HashSet<LogEntry>();
        }

        public int CustId { get; set; }
        public string CustName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<LogEntry> LogEntries { get; set; }
    }
}
