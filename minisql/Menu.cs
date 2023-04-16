using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minisql
{
    public class Menu
    {
        public static void MainMenu() //this is the main menu of the application, and where 
            //all the calls will go back to incase the user gives wrong input, i have done it 
            //this way to avoid bugs with looping and not having the user stuck if they
            //want to go back after choosing a option in the menu.
        {
            while (true)
            {   //list of all the options
                Console.Clear();
                Console.WriteLine("1. Show all projects");
                Console.WriteLine("2. Add time for a project you worked on");
                Console.WriteLine("3. Assign a person to a project");
                Console.WriteLine("4. Create person");
                Console.WriteLine("5. Create a project");
                Console.WriteLine("e. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Functions.GetAllPP();
                        break;
                    case "2":
                        Functions.RegisterTime();
                        break;
                    case "3":
                        Functions.AssignPP();
                        break;
                    case "4":
                        Functions.AddPerson();
                        break;
                    case "5":
                        Functions.AddProject();
                        break;
                    case "e": //if the user types in e and hits enter the app will close
                        Console.Clear();
                        Console.WriteLine("Closing down...");
                        Environment.Exit(0);
                        break;
                    default: //if the user types in anything other then the case: options 
                        //this will run
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
