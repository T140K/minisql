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
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //-----------------------------------------------------------------------

        public static List<ProjectPerson> GetProjectPeople()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "SELECT pp.hours_worked, p.project_name, per.name " +
                             "FROM dwr_project_person pp" +
                             " JOIN dwr_project p ON pp.project_id = p.id" +
                             " JOIN dwr_person per ON pp.person_id = per.id" +
                             " ORDER BY p.project_name ASC";

                var output = cnn.Query(sql, new DynamicParameters());
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
            {
                string sql = ($"SELECT id from dwr_project WHERE project_name = '{proj}'");
                int id = cnn.QuerySingleOrDefault<int>(sql, new { proj });
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
            {
                string sql = $"SELECT COUNT(*) FROM dwr_project_person WHERE project_id = {projId} AND person_id = {nameId}";
                int count = cnn.QuerySingle<int>(sql);

                if (count == 0)
                {
                    Console.WriteLine("Wrong project name combination!\n Returning to the main menu");
                    Console.ReadLine();
                    Menu.MainMenu();
                }

                cnn.Execute($"UPDATE dwr_project_person SET hours_worked = hours_worked + {hours} WHERE project_id = {projId} AND person_id = {nameId}");
            }
        }
        public static void AddPersonQ(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = $"SELECT COUNT(*) FROM dwr_person WHERE name = '{name}'";
                int count = cnn.QuerySingle<int>(sql);
                if (count > 0)
                {
                    Console.WriteLine("Cannot have duplicate names!\n Press any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }
                else if (name == null)
                {
                    Console.WriteLine("The name cannot be empty! \n Press any key to return to the main menu...");
                    Console.ReadLine();
                    Menu.MainMenu();
                }

                cnn.Execute($"INSERT INTO dwr_person (name) VALUES ('{name}');");
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
                    Console.WriteLine("Cannot have duplicate project names!\nPress any key to return to the main menu...");
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
            }
        }
    }
}
