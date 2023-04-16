﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minisql
{
    public class Menu
    {
        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Show all projects");
                Console.WriteLine("2. Register time for a project");
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

                        break;
                    case "e":
                        Console.Clear();
                        Console.WriteLine("Closing down...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadLine();
                        MainMenu();
                        break;
                }
            }
        }
    }
}