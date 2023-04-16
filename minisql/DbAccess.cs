using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace minisql
{
    public class DbAccess
    {//gets the connectionstring from a app.config file inside the minisql folder and saves it as
     //this function LoadConnectionString
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public static List<ProjectPerson> GetProjectPeople() //gets a list based on the
         //ProjectPerson model
        {   //connects to the db with the connectionstring function using npgsql from nuget
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {//stores a query in the "" as string sql
                                //selects all relevant columns
                string sql = "SELECT pp.hours_worked, p.project_name, per.name" +
                            //from this table that is shortend AS pp
                             " FROM dwr_project_person pp" +
                             //joins the relevant table AS p the pp table as id in p
                             " JOIN dwr_project p ON pp.project_id = p.id" +
                             //joins another relevant table AS per so now all the tables are
                             //connected with foreign keys on pp.project_id as p.project_id
                             //and pp.person_id as per.id
                             " JOIN dwr_person per ON pp.person_id = per.id" +
                             //this orders the rerival so that it is sorted by ascending letters
                             //so the database if filterd by project so you can easily see
                             //what person is connected to what project in the foreach
                             " ORDER BY p.project_name ASC";
                //output becomes the result of the query run against the database
                var output = cnn.Query(sql, new DynamicParameters()); 
                //that result is then stored in the ProjectPerson list with the lambda expression
                //so that the x.hours_worked becomes a part of a list that uses the get set
                //from Models class
                return output.Select(x => new ProjectPerson
                {
                    hours_worked = x.hours_worked,
                    project_name = x.project_name,
                    person_name = x.name

                }).ToList();
            }
        }

        public static int CheckProj(string proj)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {   //this query gets a id from this table where the name in the db matches the 
                //user input
                string sql = ($"SELECT id from dwr_project WHERE project_name = '{proj}'");
                //int id becomes the result of the query if there is a match
                int id = cnn.QuerySingleOrDefault<int>(sql, new { proj });
                //returns the id as a int when you call the function witht he right parameters
                return id;
            }
        }

        public static int CheckName(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = ($"SELECT id from dwr_person WHERE name = '{name}'");
                int id = cnn.QuerySingleOrDefault<int>(sql, new { name });
                return id;
            }
        }

        public static void RegisterTimeQ(int hours, int? projId, int? nameId)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {   //if there is a row in the db that has both the inputted nameId and project Id
                //the count will rise from 0 to 1
                string sql = $"SELECT COUNT(*) FROM dwr_project_person WHERE project_id = {projId} AND person_id = {nameId}";
                int count = cnn.QuerySingle<int>(sql);

                //if the count doesnt rise from 0 to 1 the if statement will take effect
                if (count == 0)
                {//if this if statement gets executed the user will be returned to the main menu
                    Console.WriteLine("Wrong project name combination!\n Returning to the main menu");
                    Console.ReadLine();
                    Menu.MainMenu();
                }
                //if there was a match the row with the matching IDs will get hours added
                //from previous user input
                cnn.Execute($"UPDATE dwr_project_person SET hours_worked = hours_worked + {hours} WHERE project_id = {projId} AND person_id = {nameId}");
            }
        }
        public static void AddPersonQ(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {   //checks if there is a name matching witht heuser input
                string sql = $"SELECT COUNT(*) FROM dwr_person WHERE name = '{name}'";
                int count = cnn.QuerySingle<int>(sql);
                if (count > 0)
                {   //if there is a matching name user gets sent back
                    Console.WriteLine("Cannot have duplicate names!\n Press any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }
                else if (name == null)
                {   //if name is empty user gets sent back
                    Console.WriteLine("The name cannot be empty! \n Press any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }

                cnn.Execute($"INSERT INTO dwr_person (name) VALUES ('{name}');");
                Console.WriteLine("Success\n");
            }
        }
        public static void AddProjectQ(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = $"SELECT COUNT(*) FROM dwr_project WHERE project_name = '{name}'";
                int count = cnn.QuerySingle<int>(sql);
                if (count > 0)
                {
                    Console.WriteLine("Project already exists!\nPress any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }
                else if (name == null)
                {
                    Console.WriteLine("The name cannot be empty! \n Press any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }

                cnn.Execute($"INSERT INTO dwr_project (project_name) VALUES ('{name}');");
                Console.WriteLine("Success\n");
            }
        }
        public static void AssignPP(int name, int proj)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = $"SELECT COUNT(*) FROM dwr_project_person WHERE project_id = {proj} AND person_id = {name}";
                int count = cnn.QuerySingle<int>(sql);

                if (count > 0)
                {
                    Console.WriteLine("This person is already assigned to this project!\nPress any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }

                cnn.Execute($"INSERT INTO dwr_project_person (project_id, person_id) VALUES ('{proj}', '{name}')");
                Console.WriteLine("Success\n");
            }
        }
    }
}
