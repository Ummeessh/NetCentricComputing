using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApp5ByUmesh.Models;

namespace WebApp5ByUmesh.Controllers
{
    public class StudentController : Controller
    {
        private readonly string connectionString = "Server=localhost,1433;Database=CollegeDB;User Id=sa;Password=Umesh@12345#;TrustServerCertificate=True;";

        public StudentController()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Students')
                                 BEGIN
                                     CREATE TABLE Students (
                                         Id INT PRIMARY KEY IDENTITY(1,1),
                                         Name NVARCHAR(50) NOT NULL,
                                         Faculty NVARCHAR(50) NOT NULL
                                     );
                                 END";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // READ (INDEX)
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Name, Faculty FROM Students";
                using (var cmd = new SqlCommand(query, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Faculty = reader.GetString(2)
                        });
                    }
                }
            }
            return View(students);
        }

        // CREATE
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Student student)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Students (Name, Faculty) VALUES (@Name, @Faculty)";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Faculty", student.Faculty);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        // UPDATE
        public IActionResult Edit(int id)
        {
            Student? student = null;
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Name, Faculty FROM Students WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Faculty = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return student == null ? NotFound() : View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Students SET Name = @Name, Faculty = @Faculty WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", student.Id);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Faculty", student.Faculty);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Students WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}