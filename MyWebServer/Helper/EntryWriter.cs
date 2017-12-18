using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Helper
{
    /// <summary>
    /// Used to write entries to the database
    /// </summary>
    public class EntryWriter
    {
        /// <summary>
        /// Writes randomly generated temperatures and datetimes to the database every 10 minutes
        /// </summary>
        public static void Write()
        {
            var start = new DateTime(2007, 1, 1);
            var end = DateTime.Today;
            var database = new Database();
            while (true)
            {
                System.Threading.Thread.Sleep(10 * 60 * 1000);
                database.AddEntry(GetRandomTemp(), GetRandomDateTime(start, end));
            }
        }

        private static DateTime GetRandomDateTime(DateTime start, DateTime end)
        {
            var random  = new Random();
            var range = (end - start).Days;
            return start.AddDays(random.Next(range)).AddSeconds(random.Next(86400));
        }

        private static float GetRandomTemp()
        {
            var random = new Random();
            return random.Next(51);
        }
    }
}
