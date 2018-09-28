using System;
using System.Text;
using System.Threading.Tasks;
using Function;

namespace SaveToDB
{
    public static class Program
    {
        private static string GetStdin()
        {
            StringBuilder buffer = new StringBuilder();
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                buffer.AppendLine(s);
            }
            return buffer.ToString();
        }

        static async Task Main(string[] args)
        {
            string buffer = GetStdin();
            FunctionHandler f = new FunctionHandler();

            var host = Environment.GetEnvironmentVariable("TIMESCALE_HOST") ?? "172.23.0.2";
            var port = Environment.GetEnvironmentVariable("TIMESCALE_PORT") ?? "5432";
            var username = Environment.GetEnvironmentVariable("TIMESCALE_USERNAME") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("TIMESCALE_PASSWORD") ?? "password";
            var database = Environment.GetEnvironmentVariable("TIMESCALE_DATABASE") ?? "sensors";

            var connString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";

            string responseValue = await f.Handle(buffer, connString);

            if (responseValue != null)
            {
                Console.Write(responseValue);
            }
        }
    }
}
