using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BuildBakery
{
    public class Utility
    {
        public static void SlowSqlCall()
        {
            Random rnd = new Random();
            int delay = rnd.Next(2, 7);

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["bakery"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand($"WAITFOR DELAY '00:00:0{delay}'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void MakeDatabaseCallAndFetchData()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["orders"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from NewProducts", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static async Task CallWebApi()
        {

            Random rnd = new Random();
            int delay = rnd.Next(5, 13); // creates a number between 1 and 12

            HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.162 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");

            var clientcontacts = Client.GetStringAsync("https://httpbin.org/delay/" + delay);

            await Task.WhenAll(clientcontacts);
        }

        internal static void ThrowException()
        {
            throw new ApplicationException("Cannot let you query this record");
        }

        public static void MakeOutboundConnections()
        {
            (new WebClient()).OpenRead("http://www.bing.com/");
        }
    }
}