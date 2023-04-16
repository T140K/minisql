namespace minisql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Here is a list of all projects and people working on them");
            List<ProjectPerson> ProjectsPersons = DbAccess.GetProjectPeople();
            foreach (ProjectPerson p in ProjectsPersons)
            {
                Console.WriteLine($"{p.project_name} - {p.person_name}: {p.hours_worked} hours");
            }
        }
    }
}