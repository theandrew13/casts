using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASTSvFinal
{
    class DepartmentClass
    {
        private int dept_id;
        public int Dept_id
        {
            get { return dept_id; }
            set { dept_id = value; }
        }

        private string department_name;
        public string Department_name
        {
            get { return department_name; }
            set { department_name = value; }
        }
    }
}
