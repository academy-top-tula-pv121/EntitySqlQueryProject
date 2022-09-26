using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntitySqlQueryProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AppContext app = new())
            {
                app.Database.EnsureDeleted();
                app.Database.EnsureCreated();

                List<Group> groups = new List<Group>();
                groups.Add(new Group() { Title = "PV121" });
                groups.Add(new Group() { Title = "SPD221" });
                app.Groups.AddRange(groups);

                List<Student> students = new List<Student>();
                students.Add(new Student() { Name = "Bob", Age = 23, Group = groups[0] });
                students.Add(new Student() { Name = "Tim", Age = 44, Group = groups[0] });
                students.Add(new Student() { Name = "Joe", Age = 31, Group = groups[1] });
                students.Add(new Student() { Name = "Sam", Age = 28, Group = groups[1] });
                students.Add(new Student() { Name = "Pit", Age = 36, Group = groups[1] });
                students.Add(new Student() { Name = "Tim", Age = 26, Group = groups[1] });
                app.Students.AddRange(students);

                app.SaveChanges();
            }

            using (AppContext app = new())
            {
                string sgr = "VBD321";

                app.Database.ExecuteSqlRaw($"INSERT INTO Groups (Title) VALUES ('{sgr}')");

                foreach (var g in app.Groups)
                    Console.WriteLine(g.Title);


                //var students = app.Students.FromSqlRaw("SELECT * FROM Students").OrderBy(s => s.Name).ToList();



                //SqlParameter sqlParameter = new("@name", "%Tim%");
                //var students2 = app.Students.FromSqlRaw("SELECT * FROM Students WHERE Name LIKE @name", sqlParameter).ToList();

                //SqlParameter sqlParameter3 = new("@age", "30");
                //var students3 = app.Students.FromSqlRaw("SELECT * FROM Students WHERE Age > @age", sqlParameter3).ToList();


                //foreach (var student in students)
                //    Console.WriteLine($"{student.Name} {student.Age}");
            }
        }
    }
}