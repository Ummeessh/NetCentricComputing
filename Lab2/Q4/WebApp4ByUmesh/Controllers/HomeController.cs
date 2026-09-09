using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp4ByUmesh.Models;

namespace WebApp4ByUmesh.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");

        public IActionResult Index()
        {
            // 1. WITHOUT FILE (In-Memory String)
            var sampleStudent = new Student { Id = 101, Name = "Umesh", Faculty = "CSIT" };
            
            // Serialize object to JSON string
            string jsonString = JsonSerializer.Serialize(sampleStudent, new JsonSerializerOptions { WriteIndented = true });
            
            // Deserialize JSON string back to object
            Student studentFromString = JsonSerializer.Deserialize<Student>(jsonString);


            // 2. WITH FILE (data.json)
            // Write JSON string to file
            System.IO.File.WriteAllText(_filePath, jsonString);

            // Read JSON string from file
            string jsonFromFile = System.IO.File.ReadAllText(_filePath);
            
            // Deserialize JSON from file contents back to object
            Student studentFromFile = JsonSerializer.Deserialize<Student>(jsonFromFile);


            // Pass data to View
            ViewBag.InMemoryJson = jsonString;
            ViewBag.DeserializedInMemory = studentFromString;
            ViewBag.FileJson = jsonFromFile;
            ViewBag.DeserializedFromFile = studentFromFile;

            return View();
        }
    }
}