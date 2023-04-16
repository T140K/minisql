using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minisql
{
    public class ProjectPerson
    {
        public int project_id { get; set; }
        public int person_id { get; set; }
        public int hours_worked { get; set; }
        public string project_name { get; set; }
        public string person_name { get; set; }
    }
}
