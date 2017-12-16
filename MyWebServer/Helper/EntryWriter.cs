﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Helper
{
    public class EntryWriter
    {
        public static void Write()
        {
            var start = new DateTime(1970, 1, 1);
            var end = DateTime.Today;
            var database = new Database();
            while (true)
            {
                database.AddEntry(GetRandomTemp(), GetRandomDateTime(start, end));
            }
        }

        private static DateTime GetRandomDateTime(DateTime start, DateTime end)
        {
            var random  = new Random();
            var range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static float GetRandomTemp()
        {
            var random = new Random();
            return random.Next(51);
        }
    }
}