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

        public static List<ProjectPerson> GetProjectPeople()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "SELECT pp.hours_worked, p.project_name, per.name, per.password " +
                             "FROM dwr_project_person pp" +
                             " JOIN dwr_project p ON pp.project_id = p.id" +
                             " JOIN dwr_person pe ON pp.person_id = pe.id" +
                             " ORDER BY p.project_name ASC";

                var output = cnn.Query(sql, new DynamicParameters());
                return output.Select(x => new ProjectPerson
                {
                    hours_worked = x.hours_worked,
                    project_name = x.project_name,
                    person_name = x.name,
                    password = x.password

                }).ToList();
            }
        }
    }
}
