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
            //this function calls for a freash list of all hits from the query that is run
            //in DbAccess
        {
            Console.Clear();
            Console.WriteLine("Here is a list of all projects and people working on them");
            List<ProjectPerson> ProjectsPersons = DbAccess.GetProjectPeople(); //this gets a 
            //fresh list
            foreach (ProjectPerson p in ProjectsPersons)//displays the list
            {
                Console.WriteLine($"{p.project_name} - {p.person_name}: {p.hours_worked} hours");
            }
            Console.ReadLine();
        }
        public static void RegisterTime()//takes in a name from the user and gives back
                                         //the id of the person
        {
            Console.WriteLine("What project would you like to register time on?");
            string proj = Console.ReadLine();
            int? projId = DbAccess.CheckProj(proj); //sends out the user input to Function
            if (projId == 0)//if the id is 0 it means that nothing was returned from the db
                //as the index starts at 1
            {
                Console.WriteLine("Wrong project name, returning to main menu");
                Console.ReadLine();
                Menu.MainMenu();//returns you back to the main menu instead of forcing you
                //to keep trying to input the right name of a project
            }

            Console.WriteLine("What is your name?");//takes in user input as name,checks name
            //against names in db and if there is a match you get back the id for that name
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
            //sends out the hours you input, project name you input and name you input
            DbAccess.RegisterTimeQ(hours, projId, nameId);
            //confirmation messege
            Console.WriteLine($"Added {hours} hours to the project!\n Press any key to continiue...");
            Console.ReadLine();
        }
        public static void AddPerson()//menu option for adding people
        {
            Console.WriteLine("What is the name of the person you want to add?");
            string nameCheck = Console.ReadLine();
            if (!ContainsOnlyLetters(nameCheck))//calls a function that reutrns true if you
            //you only use letters and spaces
            {
                Console.WriteLine("Name can only contain letters, returning to the main menu...");
                Console.ReadLine();
                Menu.MainMenu();
            }

            string name = nameCheck; //i made a new string to improve readability

            DbAccess.AddPersonQ(name);
            Console.WriteLine($"Success, {name} has been added!\n Press any key to return to the main menu...");
            Console.ReadLine();
        }
        //the function that checks if the string only has letters and spaces
        public static bool ContainsOnlyLetters(string input)
        {
            Regex regex = new Regex("^[a-zA-Z ]+$");//eveything inside the [] is what is
            //allowed so a space, small letters a to z and big letters A to Z
            return regex.IsMatch(input);//checks if input matches the requirement for regex
        }
        public static bool ContainsOnlyLettersAndNumbers(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]+$");//same as above but with numbers too
                                                       //as it has 0-9 in the []
            return regex.IsMatch(input);
        }
        public static void AddProject()
        {
            Console.WriteLine("What is the name of the project you want to add?");
            string name = Console.ReadLine();

            if (!ContainsOnlyLettersAndNumbers(name))//check that allows numbers as above
            {//nothing new from here down
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
