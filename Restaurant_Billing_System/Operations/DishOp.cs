using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Billing_System.DB_Prop;
using Microsoft.EntityFrameworkCore;

namespace Restaurant_Billing_System.Operations
{
    public class DishOp
    {
        Validation validation = new Validation();
        RestaurantContext rtc;
        public DishOp()
        {
            rtc = new RestaurantContext();
        }

        public async Task MenuOp()
        {
            try
            {
                int a = 0;
                do
                {
                    Console.WriteLine("\nMenu Operations\n");
                    Console.WriteLine("Please enter Your Choice");
                    Console.WriteLine("1. View Menu\n" +
                        "2. Update Dish\n" +
                        "3. Add Dish\n" +
                        "4. Delete Dish\n" +
                        "5. Clear Screen\n" +
                        "6. Exit Menu Operations");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ViewCompleteMenu();
                            break;
                        case 2:
                            ViewCompleteMenu();
                            Dish dish = new Dish();
                            Console.WriteLine("Enter Details");
                            Console.WriteLine("Enter Dish ID");
                            dish.DishId = validation.IsPositiveNumber();
                            var searchDish = await GetDishAsync(dish.DishId);
                            int e = 0;
                            do
                            {
                                try
                                {
                                    if (searchDish == null)
                                    {
                                        Console.WriteLine("Please Enter Correct Dish ID");
                                        dish.DishId = validation.IsPositiveNumber();
                                        searchDish = await GetDishAsync(dish.DishId);
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
                            Console.WriteLine("Enter Dish Name");
                            dish.DishName = validation.IsCorrectName();
                            Console.WriteLine("Enter Price");
                            dish.Price = validation.IsPositiveNumber();
                            var updateDish = await UpdateAsync(dish.DishId, dish);
                            if (updateDish == null) { Console.WriteLine("Failed To Update Dish Item"); }
                            else { Console.WriteLine($"Dish with ID = {dish.DishId} is Updated"); }
                            break;
                        case 3:
                            Dish dish1 = new Dish();
                            Console.WriteLine("Enter Details");
                            Console.WriteLine("Enter Dish Name");
                            dish1.DishName = validation.IsCorrectName();
                            Console.WriteLine("Enter Price");
                            dish1.Price = validation.IsPositiveNumber();
                            var addDish = await CreateAsync(dish1);
                            if (addDish == null) { Console.WriteLine("Failed To Add Dish Item"); }
                            else { Console.WriteLine($"Dish with ID = {dish1.DishId} is Added"); }
                            break;
                        case 4:
                            Console.WriteLine("Enter Dish ID");
                            int ID = validation.IsPositiveNumber();
                            var deleteDish = await DeleteAsync(ID);
                            if (deleteDish == null) { Console.WriteLine("Failed To Delete Dish Item"); }
                            else { Console.WriteLine($"Dish with ID = {ID} is Deleted"); }
                            break;
                        case 5:
                            Console.Clear();
                            break;
                        case 6:
                            a++;
                            Console.Clear();
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
        }

        public async Task<Dish> GetDishAsync(int id)
        {
            try
            {
                var result = await rtc.Dishes.FindAsync(id);
                if (result == null)
                {
#pragma warning disable CS8603 // Possible null reference return.
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

        async Task<Dish> CreateAsync(Dish entity)
        {
            try
            {
                var result = await rtc.Dishes.AddAsync(entity);
                await rtc.SaveChangesAsync();
                return result.Entity; // Return newly CReated ENtity
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        async Task<Dish> DeleteAsync(int id)
        {
            try
            {
                var dishToFind = await rtc.Dishes.FindAsync(id);
                if (dishToFind == null)
                {
                    return null;
                }
                rtc.Dishes.Remove(dishToFind);
                await rtc.SaveChangesAsync();
                return dishToFind;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Dish>> GetAsync()
        {
            try
            {
                var result = await rtc.Dishes.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        async Task<Dish> UpdateAsync(int id, Dish entity)
        {
            try
            {
                var dishUpdate = await rtc.Dishes.FindAsync(id);
                if (dishUpdate == null)
                {
                    return null;
                }
                dishUpdate.DishName = entity.DishName;
                dishUpdate.Price = entity.Price;
                await rtc.SaveChangesAsync();
                return dishUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void ViewMenu()
        {
            var menu = GetAsync().Result;
            Console.WriteLine("\n**** Menu ****");
            Console.WriteLine("ID \t  Name ");
            foreach (Dish item in menu)
            {
                Console.WriteLine($"{item.DishId} \t {item.DishName} ");
            }
        }

        public void ViewCompleteMenu()
        {
            var viewMenu = GetAsync().Result;
            Console.WriteLine("\nMenu\n");
            Console.WriteLine("ID\t   Name\t       Price");
            foreach (Dish item in viewMenu)
            {
                Console.WriteLine($" {item.DishId}\t  {item.DishName}\t    {item.Price}");
            }
        }

    }
}

