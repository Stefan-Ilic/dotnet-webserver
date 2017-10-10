using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebServer
{
    public static class Utility
    {
        public static int CountParams(string url)
        {
            bool containsParams = false;
            int count = 0;

            foreach (char c in url)
            {
                if (c == '?')
                {
                    containsParams = true;
                    count++;
                }
            }
            if (containsParams)
            {
                foreach (char c in url)
                {
                    if (c == '&')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
