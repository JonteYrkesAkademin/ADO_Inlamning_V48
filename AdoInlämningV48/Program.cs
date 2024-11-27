using Microsoft.Data.SqlClient;

namespace AdoInlämningV48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endProgram = true;
            while (endProgram)
            {
                Console.WriteLine("Sök efter en skådespelare och se vilka filmer den varit med i:");

                Console.WriteLine("Skriv in vilket förnamn du vill söka på:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Skriv in vilket efternamn du vill söka på:");
                string lastName = Console.ReadLine();

                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine("Hämtar filmer......\n");


                var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                string query = $"SELECT f.title AS InMovie " +
                    $"FROM film f " +
                    $"INNER JOIN film_actor fa ON f.film_id = fa.film_id " +
                    $"INNER JOIN actor a ON fa.actor_id = a.actor_id " +
                    $"WHERE a.first_name = '{firstName}' AND a.last_name = '{lastName}'";
                var command1 = new SqlCommand(query, connection);

                connection.Open();

                var rec = command1.ExecuteReader();

                if (rec.HasRows)
                {
                    while (rec.Read())
                    {
                        Console.WriteLine(rec[0]);
                    }
                }

                connection.Close();

                Console.WriteLine("Vill du söka på en skådespelare till? (y/n)");
                string answer = Console.ReadLine().ToLower();
                if (answer != "y")
                {
                    endProgram = false;
                }

            }
        }
    }
}
