using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant_Billing_System.Operations
{
    public class Validation
    {
        public int IsPositiveNumber()
        {
            int number = Convert.ToInt32(Console.ReadLine());
            int d = 0;
            do
            {
                try
                {
                    if (number >= 0)
                    {
                        d = 0;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a positive number");
                        number = Convert.ToInt32(Console.ReadLine());
                        d++;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (d > 0);
            return number;
        }

        public string IsCorrectName()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Name = Console.ReadLine();
            Regex re = new Regex("[A-Z][A-Za-z ]+[A-Za-z]$");
            int g = 0;
            do
            {
#pragma warning disable CS8604 // Possible null reference argument.
                if (re.IsMatch(Name))
                {
                    g = 0;
                }
                else
                {
                    Console.WriteLine("Please enter correct name");
                    Name = Console.ReadLine();
                    g++;
                }
#pragma warning restore CS8604 // Possible null reference argument.
            } while (g > 0);
#pragma warning disable CS8603 // Possible null reference return.
            return Name;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public string IsCorrectMobileNum()
        {
            string Mob = Console.ReadLine();
            Regex re = new Regex(@"^[0-9]{10}$");
            int g = 0;
            do
            {
#pragma warning disable CS8604 // Possible null reference argument.
                if (re.IsMatch(Mob))
                {
                    g = 0;
                }
                else
                {
                    Console.WriteLine("Please enter correct Mobile Number");
                    Mob = Console.ReadLine();
                    g++;
                }
            } while (g > 0);
#pragma warning disable CS8603 // Possible null reference return.
            return Mob;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public string IsPayment()
        {
            string str = Console.ReadLine();
            int x = 0;
            do
            {
                if (str == "UPI" || str == "Card" || str == "Cash")
                {
                    x = 0;
                }
                else
                {
                    Console.WriteLine("Enter Payment Method (UPI, Card, Cash)");
                    str = Console.ReadLine(); 
                    x++;
                }
            } while (x > 0);
#pragma warning disable CS8603 // Possible null reference return.
            return str;

        }

        public int IsTableNo()
        {
            int number = Convert.ToInt32(Console.ReadLine());
            int d = 0;
            do
            {
                try
                {
                    if (number >= 1 && number <= 50)
                    {
                        d = 0;
                    }
                    else
                    {
                        Console.WriteLine("Please enter Table No between 1 to 50");
                        number = Convert.ToInt32(Console.ReadLine());
                        d++;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (d > 0);
            return number;
        }
    }
}
