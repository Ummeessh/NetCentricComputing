using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using WebApp5ByUmesh.Models;

namespace WebApp5ByJayson.Controllers
{
    public class StudentController : Controller
    {
        private readonly string connectionString = "Data Source=CollegeDB.db;";

        public StudentController()
        {
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = @"CREATE TABLE IF NOT EXISTS Students (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT NOT NULL,
                                    Faculty TEXT NOT NULL
                                );";
                using (var cmd = new SqliteCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // READ (INDEX)
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Name, Faculty FROM Students";
                using (var cmd = new SqliteCommand(query, con))
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
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Students (Name, Faculty) VALUES (@Name, @Faculty)";
                using (var cmd = new SqliteCommand(query, con))
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
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Name, Faculty FROM Students WHERE Id = @Id";
                using (var cmd = new SqliteCommand(query, con))
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
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Students SET Name = @Name, Faculty = @Faculty WHERE Id = @Id";
                using (var cmd = new SqliteCommand(query, con))
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
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Students WHERE Id = @Id";
                using (var cmd = new SqliteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}