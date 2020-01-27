using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (var context = new StudentContext())
            {
#if DEBUG
                // Log query generated to output window
                context.Database.Log = Console.WriteLine;
#endif

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

        public static Student Add(Student stu)
        {
            using (var context = new StudentContext())
            {
#if DEBUG
                // Log query generated to output window
                context.Database.Log = Console.WriteLine;
#endif
                // Generating/preparing insert query
                context.Students.Add(stu);
                // Executing insert query against DB
                context.SaveChanges();

                // Return the student with the StudentId set
                return stu;
            }

        }

        /// <summary>
        /// Deletes a student from the database by their StudentId
        /// </summary>
        /// <param name="s"></param>
        public static void Delete(Student s)
        {
            using(StudentContext context = new StudentContext())
            {
#if DEBUG
                // Log query generated to output window
                context.Database.Log = Console.WriteLine;
#endif
                context.Students.Attach(s);
                context.Entry(s).State = EntityState.Deleted;
                context.SaveChanges();
            }

            // This is what a using statement generates
            /*
            var context2 = new StudentContext();
            try
            {
                //DB Code goes here
            }
            finally
            {
                context2.Dispose();
            }
            */
        }

        /// <summary>
        /// Updates all student data (except for PK)
        /// </summary>
        /// <param name="s">The student to be updated</param>
        public static Student Update(Student s)
        {
            using (var context = new StudentContext())
            {
#if DEBUG
                context.Database.Log = Console.WriteLine;
#endif
                context.Students.Attach(s);
                context.Entry(s).State = EntityState.Modified;
                context.SaveChanges();

                return s;
            }
        }

        /// <summary>
        /// If StudentId is 0 they will be added
        /// otherwise it will update based on the StudentId
        /// </summary>
        /// <param name="s">The student to be added or updated</param>
        public static Student AddOrUpdate(Student s)
        {
            using(var context = new StudentContext())
            {
                context.Entry(s).State = (s.StudentId == 0) ? EntityState.Added : 
                                                              EntityState.Modified;
                context.SaveChanges();

                return (s);
            }
        }
    }
}
