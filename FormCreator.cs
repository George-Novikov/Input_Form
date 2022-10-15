using Microsoft.Data.SqlClient;
using Input_Form.Models;

namespace Input_Form
{
    public class FormCreator
    {
        public static string connectionString = @"Server=(localdb)\mssqllocaldb;Database=InputForms;Trusted_Connection=True";
        public Form LoadFormFormDb()
        {
            Form form = new Form();
            string sqlExpression = "SELECT TOP 1 * FROM TABLE Forms ORDER BY FormId DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(sqlExpression);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    }
                }
            }
            return form;
        }
    }
}
