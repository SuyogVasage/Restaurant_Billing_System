using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant_Billing_System.DB_Prop;

namespace restaurant_billing_system.operations
{
    public class BillOp
    {
        RestaurantContext context;
        public BillOp()
        {
            context = new RestaurantContext();
        }

        public async Task<Customer> CreateCutomerAsync(Customer entity)
        {
            try
            {
                var result = await context.Customers.AddAsync(entity);
                await context.SaveChangesAsync();
                return result.Entity; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
#pragma warning disable CS8603 // Possible null reference return.
                return null;
            }
        }
        public async Task<LogEntry> CreateLogAsync(LogEntry entity)
        {
            try
            {
                var result = await context.LogEntries.AddAsync(entity);
                await context.SaveChangesAsync();
                return result.Entity; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Bill> CreateBillAsync(Bill entity)
        {
            try
            {
                var result = await context.Bills.AddAsync(entity);
                await context.SaveChangesAsync();
                return result.Entity; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Customer> GetCustomerAsync(int id)
        {
            try
            {
                var result = await context.Customers.FindAsync(id);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Bill>> GetBillAsync()
        {
            try
            {
                var result = await context.Bills.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void GetBill(int ID)
        {
            var getLog = context.LogEntries.Where(x => x.CustId == ID);
            var getbill = context.Bills.Where(x => x.CustId == ID);
            Console.WriteLine();
            Console.WriteLine("******************************************************");
            Console.WriteLine("\t        ~~~~ Hotel Suyog ~~~");
            Console.WriteLine("\t           ~~~~ Bill ~~~~");
            Console.WriteLine("******************************************************");
            foreach (var item in getbill)
            {
                Console.WriteLine($"Bill No :- {item.BillNo}\n" +
                    $"Customer ID :- {item.CustId}  Customer Name :- {item.CustName}\n" +
                    $"Mobile No :- {item.MobileNo}   Time :- {item.Date}");
            }
            Console.WriteLine("******************************************************");
            Console.WriteLine("Dish Name   Price  Qty  Total");
            foreach (var item in getLog)
            {
                Console.WriteLine($"{item.DishName}      {item.Price}     {item.Quantity}     {item.Value}");
            }
            Console.WriteLine("******************************************************");
            foreach (var item in getbill)
            {
                Console.WriteLine($"SubTotal :- {item.Subtotal}\n" +
                    $"Tax 5% GST :- {item.Tax}\n" +
                    $"Final Total :- {item.FinalTotal}\n" +
                    $"Payment Gateway :- {item.Payment}");
            }
            Console.WriteLine("******************************************************");
            Console.WriteLine("\t    Thank You Visit Again :)");
            Console.WriteLine("******************************************************");
            Console.WriteLine();
        }
        public void GetBill()
        {
            var getbill = context.Bills.ToList();
            Console.WriteLine("******************************************************");
            Console.WriteLine("\t        ~~~~ Hotel Suyog ~~~");
            Console.WriteLine("\t           ~~~~ Bill ~~~~");
            Console.WriteLine("******************************************************");
            foreach (var item in getbill)
            {
                Console.WriteLine($"Bill No :- {item.BillNo}\n" +
                    $"Customer ID :- {item.CustId}  Customer Name :- {item.CustName}\n" +
                    $"Mobile No :- {item.MobileNo}   Time :- {item.Date}");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"SubTotal :- {item.Subtotal}\n" +
                    $"Tax 5% GST :- {item.Tax}\n" +
                    $"Final Total :- {item.FinalTotal}\n" +
                    $"Payment Gateway :- {item.Payment}");
                Console.WriteLine("******************************************************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("******************************************************");
            }
            
        }

    }
}
