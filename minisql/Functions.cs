using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace minisql
{
    public class Functions
    {
        public static void GetAllPP()
        {
            Console.Clear();
            Console.WriteLine("Here is a list of all projects and people working on them");
            List<ProjectPerson> ProjectsPersons = DbAccess.GetProjectPeople();
            foreach (ProjectPerson p in ProjectsPersons)
            {
                Console.WriteLine($"{p.project_name} - {p.person_name}: {p.hours_worked} hours");
            }
            Console.ReadLine();
        }
        public static void RegisterTime()
        {
            Console.WriteLine("What project would you like to register time on?");
            string proj = Console.ReadLine();
            int? projId = DbAccess.CheckProj(proj);
            if (projId == 0)
            {
                Console.WriteLine("Wrong project name, returning to main menu");
                Console.ReadLine();
                Menu.MainMenu();
            }

            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            int? nameId = DbAccess.CheckName(name);
            if (nameId == 0)
            {
                Console.WriteLine("Wrong name, returning to main menu");
                Console.ReadLine();
                Menu.MainMenu();
            }

            Console.WriteLine("How many hours did you want to add to the project?");
            int hours = Int32.Parse(Console.ReadLine());

            DbAccess.RegisterTimeQ(hours, projId, nameId);

            Console.WriteLine($"Added {hours} hours to the project!\n Press any key to continiue...");
            Console.ReadLine();
        }
        public static void AddPerson()
        {
            Console.WriteLine("What is the name of the person you want to add?");
            string nameCheck = Console.ReadLine();
            if (!ContainsOnlyLetters(nameCheck))
            {
                Console.WriteLine("Name can only contain letters, returning to the main menu...");
                Console.ReadLine();
                Menu.MainMenu();
            }

            string name = nameCheck;

            DbAccess.AddPersonQ(name);
            Console.WriteLine($"Success, {name} has been added!\n Press any key to return to the main menu...");
            Console.ReadLine();
        }
        public static bool ContainsOnlyLetters(string input)
        {
            Regex regex = new Regex("^[a-zA-Z ]+$");
            return regex.IsMatch(input);
        }
        public static bool ContainsOnlyLettersAndNumbers(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]+$");
            return regex.IsMatch(input);
        }
        public static void AddProject()
        {
            Console.WriteLine("What is the name of the project you want to add?");
            string name = Console.ReadLine();

            if (!ContainsOnlyLettersAndNumbers(name))
            {
                Console.WriteLine("Name can only contain letters and numbers. \nReturning to the main menu...");
                Console.ReadLine();
                Menu.MainMenu();
            }

            DbAccess.AddProjectQ(name);

            Console.WriteLine($"Success, {name} has now been added to the list, now assign people to it!");
            Console.ReadLine();
        }

        public static void AssignPP()
        {
            Console.WriteLine("Who would you like to assign to this project?");
            string nameC = Console.ReadLine();
            int name = DbAccess.CheckName(nameC);
            if (name == 0)
            {
                Console.WriteLine("Name not found, returning to main menu");
                Console.ReadLine();
                Menu.MainMenu();
            }
            Console.WriteLine("What projct do you want to assign this person to?");
            string projC = Console.ReadLine();
            int proj = DbAccess.CheckProj(projC);
            if (proj == 0)
            {
                Console.WriteLine("Project not found, returning to main menu");
                Console.ReadLine();
                Menu.MainMenu();
            }

            DbAccess.AssignPP(name, proj);

            Console.WriteLine($"Success, {nameC} has been assigen to {projC}!");
            Console.ReadLine();
        }
    }
}
