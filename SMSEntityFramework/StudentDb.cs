using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSEntityFramework
{
    public static class StudentDb
    {
        /// <summary>
        /// Returns a list of all the students
        /// sorted by StudentId in ascending order
        /// </summary>
        public static List<Student> GetAllStudents()
        {
            // var is okay to use if type is clearly stated (example uses StudentConext so it clearly states it)
            // Create instance of class
            var context = new StudentContext();

            // LINQ - Language INtegrated Query

            // LINQ Query Syntax
            List<Student> students =
                (from s in context.Students
                 orderby s.StudentId ascending
                 select s).ToList();

            // LINQ Method Syntax
            List<Student> students2 = context
                                        .Students
                                        .OrderBy(stu => stu.StudentId)
                                        .ToList();

            return students;
        }
    }
}
