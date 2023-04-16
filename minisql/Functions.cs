using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
