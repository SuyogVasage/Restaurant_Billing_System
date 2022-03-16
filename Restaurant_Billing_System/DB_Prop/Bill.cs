using System;
using System.Collections.Generic;

namespace Restaurant_Billing_System.DB_Prop
{
    public partial class Bill
    {
        public int BillNo { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public int TableNo { get; set; }
        public DateTime Date { get; set; }
        public double Subtotal { get; set; }
        public double Tax { get; set; }
        public double FinalTotal { get; set; }
        public string? Payment { get; set; }

        public virtual Customer Cust { get; set; } = null!;
    }
}
