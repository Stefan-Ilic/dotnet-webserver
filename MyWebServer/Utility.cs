using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

        /*public static string RealPath(string url)
        {
            /*string path = String.Empty;

            foreach (char c in url)
            {
                if (c != '?')
                {
                    path += c;
                }
                else
                {
                    return path;
                }
            }
            return path;
        }*/
    }
}
