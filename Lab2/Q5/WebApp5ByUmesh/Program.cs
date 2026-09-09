using Microsoft.Data.SqlClient;

namespace WebApp5ByUmesh
{
    class Program
    {
        // Connection string targeting localhost and CollegeDB
        private static string connectionString = "Server=localhost,1433;Database=CollegeDB;User Id=sa;Password=Umesh@12345#;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            // 1. CREATE (Insert)
            InsertStudent("Umesh", "CSIT");

            // 2. READ (Select)
            Console.WriteLine("--- After Insertion ---");
            ReadStudents();

            // 3. UPDATE
            UpdateStudent(1, "Umesh Pariyar", "New Faculty");

            // 4. READ (Select after update)
            Console.WriteLine("\n--- After Update ---");
            ReadStudents();

            // 5. DELETE
            DeleteStudent(3);

            // 6. READ (Select after delete)
            Console.WriteLine("\n--- After Deletion ---");
            ReadStudents();
        }

        static void InsertStudent(string name, string faculty)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (Name, Faculty) VALUES (@Name, @Faculty)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Faculty", faculty);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record inserted successfully.");
            }
        }

        static void ReadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Faculty FROM Students";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Faculty: {reader["Faculty"]}");
                }
            }
        }

        static void UpdateStudent(int id, string name, string faculty)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Name = @Name, Faculty = @Faculty WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Faculty", faculty);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record updated successfully.");
            }
        }

        static void DeleteStudent(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record deleted successfully.");
            }
        }
    }
}