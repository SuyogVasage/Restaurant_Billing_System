using Restaurant_Billing_System.Operations;
using Restaurant_Billing_System.DB_Prop;
using restaurant_billing_system.operations;

try
{
    RestaurantContext ctx = new RestaurantContext();
    Validation validation = new Validation();
    DishOp dishOp = new DishOp();
    BillOp billOp = new BillOp();
    int a = 0;
    do
    {
        Console.WriteLine();
        Console.WriteLine("  ~~~ Hotel Suyog ~~~");
        Console.WriteLine("~~~ Billing System ~~~");
        Console.WriteLine("\nPlease Enter Your Choice Number");
        Console.WriteLine("1. Menu Operations\n" +
            "2. Create Bill\n" +
            "3. View All bills\n" +
            "4. View Bills by Customer ID\n" +
            "5. Clear Screen\n" +
            "6. Exit Application\n");
        int choice = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        switch (choice)
        {
            case 1:
                Console.Clear();
                await dishOp.MenuOp();
                break;
            case 2:
                Customer customer = new Customer();
                Bill bill = new Bill();
                Dish dish = new Dish();
                Console.WriteLine("Enter Customer Name (First Letter of Name Should be Capital)");
                customer.CustName = validation.IsCorrectName();
                bill.CustName = customer.CustName;
                Console.WriteLine("Enter Customer's Mobile Number");
                customer.MobileNo = validation.IsCorrectMobileNum();
                bill.MobileNo = customer.MobileNo;
                Console.WriteLine("Enter Table Number(1 to 50)");
                bill.TableNo = validation.IsTableNo();
                bill.Date = Convert.ToDateTime(DateTime.Now);

                await billOp.CreateCutomerAsync(customer);  //Data To Customer Table

                bill.CustId = customer.CustId;
                int b = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nEnter Dish Details");
                    Console.WriteLine("1.Add Dish\n" +
                        "2.Dishes Completed\n" +
                        "3.Clean Screen");
                    int choice1 = Convert.ToInt32(Console.ReadLine());
                    switch (choice1)
                    {
                        case 1:
                            dishOp.ViewMenu();
                            LogEntry log = new LogEntry();
                            log.CustId = customer.CustId;
                            Console.WriteLine("Enter Dish ID");
                            log.DishId = validation.IsPositiveNumber();
                            var searchDish = await dishOp.GetDishAsync(log.DishId);
                            int e = 0;
                            do
                            {
                                try
                                {
                                    if (searchDish == null)
                                    {
                                        Console.WriteLine("Please enter Correct Dish ID");
                                        log.DishId = validation.IsPositiveNumber();
                                        searchDish = await dishOp.GetDishAsync(log.DishId);
                                        e++;
                                    }
                                    else
                                    {
                                        e = 0;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            } while (e > 0);
                            Console.WriteLine("Enter Quantity");
                            log.Quantity = validation.IsPositiveNumber();
                            log.Value = (double)log.Quantity * searchDish.Price;
                            log.DishName = searchDish.DishName;
                            log.Price = searchDish.Price;
                            await billOp.CreateLogAsync(log);  //Data to LogEntry Table
                            break;
                        case 2:
                            b++;
                            break;
                        case 3:
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Please Enter Correct Choice Number");
                            break;
                    }

                } while (b == 0);

                bill.Subtotal = ctx.LogEntries.Where(x => x.CustId == customer.CustId).Sum(x => x.Value);
                bill.Tax = 0.05 * bill.Subtotal;
                bill.FinalTotal = bill.Subtotal + bill.Tax;
                Console.WriteLine("Enter Payment Method (UPI, Card, Cash)");
                bill.Payment = validation.IsPayment();
                await billOp.CreateBillAsync(bill);
                billOp.GetBill(customer.CustId);
                break;
            case 3:
                billOp.GetBill();
                break;
            case 4:
                var allBill = await billOp.GetBillAsync();
                Console.WriteLine("CustID         DateTime");
                foreach (var item in allBill)
                {
                    Console.WriteLine($"{item.CustId}\t   {item.Date}");
                }
                Console.WriteLine("Enter Customer ID for Bill");
                int ID = validation.IsPositiveNumber();
                var getID = await billOp.GetCustomerAsync(ID);
                int d = 0;
                do
                {
                        if (getID == null)
                        {
                            Console.WriteLine("Please enter Correct Customer ID");
                            ID = validation.IsPositiveNumber();
                            getID = await billOp.GetCustomerAsync(ID);
                            d++;
                        }
                        else
                        {
                            d = 0;
                        }
                } while (d > 0);
                billOp.GetBill(ID);
                break;
            case 5:
                Console.Clear();
                break;
            case 6:
                a++;
                break;
            default:
                Console.WriteLine("Please Enter Correct Choice Number");
                break;
        }

    } while (a == 0);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

