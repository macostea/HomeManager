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

            string responseValue = await f.Handle(buffer);

            if (responseValue != null)
            {
                Console.Write(responseValue);
            }
        }
    }
}
