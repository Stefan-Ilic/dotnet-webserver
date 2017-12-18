using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    /// <summary>
    /// This class represents the database
    /// </summary>
    public class Database
    {
        /// <summary>
        /// The connection to the database
        /// </summary>
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// The connection string for the database connection
        /// </summary>
        public string ConnectionString { get; set; } =
            "Data Source=Ilic;Initial Catalog = Temperature; Integrated Security = True";

        /// <summary>
        /// A bool flag to determine whether the instance is a mock instance for unit testing
        /// </summary>
        public bool IsMock { get; set; } = false;

        /// <summary>
        /// Instances the database object and sets the IsMock property according to execution location
        /// </summary>
        public Database()
        {
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string jenkins = "c:\\workspace\\BIF-WS17-SWE1-if16b072\\deploy";

            IsMock = wdir == jenkins;
        }

        private void Connect()
        {
            Connection = new SqlConnection { ConnectionString = ConnectionString };
            Connection.Open();
        }

        private void Disconnect()
        {
            Connection.Close();
            Connection.Dispose();
            Connection = null;
        }

        /// <summary>
        /// Adds a Temperature and its DateTime to the database
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="dateTime"></param>
        public void AddEntry(float temperature, DateTime dateTime)
        {
            if (IsMock) return;

            Connect();

            var command = new SqlCommand(@"INSERT INTO Entry (Temperature, DateTime)
                VALUES (@temp, @datetime)", Connection);

            command.Parameters.AddWithValue("@temp", temperature);
            command.Parameters.AddWithValue("@datetime", dateTime);
            command.ExecuteNonQuery();

            Disconnect();
        }

        /// <summary>
        /// Returns a list of temperatures for a given page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<float> GetTemps(int page)
        {
            if (IsMock) return new List<float>();

            Connect();
            var command = new SqlCommand(@"SELECT Temperature FROM Entry ORDER BY id OFFSET (@skip) ROWS FETCH NEXT 8 ROWS ONLY", Connection);
            command.Parameters.AddWithValue("@skip", (page-1) * 8);
            var temperatures = new List<float>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    temperatures.Add(float.Parse($"{reader[0]}"));
                }
            }
            Disconnect();
            return temperatures;
        }

        /// <summary>
        /// Returns a list of temperatures for a given page and a given datetime
        /// </summary>
        /// <param name="page"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<float> GetTemps(int page, DateTime dateTime)
        {
            if (IsMock) return new List<float>();

            Connect();
            var command = new SqlCommand(@"SELECT Temperature FROM Entry 
                WHERE DATEPART(yy, DateTime) = @year 
                AND DATEPART(mm, DateTime) = @month 
                AND DATEPART(dd, DateTime) = @day 
                ORDER BY id 
                OFFSET (@skip) ROWS 
                FETCH NEXT 8 ROWS ONLY", Connection);
            command.Parameters.AddWithValue("@skip", (page - 1) * 8);
            command.Parameters.AddWithValue("@year", dateTime.Year);
            command.Parameters.AddWithValue("@month", dateTime.Month);
            command.Parameters.AddWithValue("@day", dateTime.Day);
            var temperatures = new List<float>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    temperatures.Add(float.Parse($"{reader[0]}"));
                }
            }
            Disconnect();
            return temperatures;
        }

        /// <summary>
        /// Returns a list of temperatures for a given datetime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<float> GetTemps(DateTime dateTime)
        {
            if (IsMock) return new List<float>();

            Connect();
            var command = new SqlCommand(@"SELECT Temperature FROM Entry 
                WHERE DATEPART(yy, DateTime) = @year 
                AND DATEPART(mm, DateTime) = @month 
                AND DATEPART(dd, DateTime) = @day 
                ORDER BY id", Connection);
            command.Parameters.AddWithValue("@year", dateTime.Year);
            command.Parameters.AddWithValue("@month", dateTime.Month);
            command.Parameters.AddWithValue("@day", dateTime.Day);
            var temperatures = new List<float>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    temperatures.Add(float.Parse($"{reader[0]}"));
                }
            }
            Disconnect();
            return temperatures;
        }

        /// <summary>
        /// Returns a list of datetimes for a given page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<DateTime> GetDateTimes(int page)
        {
            if (IsMock) return new List<DateTime>();

            Connect();
            var command = new SqlCommand(@"SELECT DateTime FROM Entry ORDER BY id OFFSET (@skip) ROWS FETCH NEXT 8 ROWS ONLY", Connection);
            command.Parameters.AddWithValue("@skip", (page - 1) * 8);
            var dateTime = new List<DateTime>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dateTime.Add(Convert.ToDateTime($"{reader[0]}"));
                }
            }
            Disconnect();
            return dateTime;
        }

        /// <summary>
        /// Returns a list of datetimes for a given page and a given datetime
        /// </summary>
        /// <param name="page"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public List<DateTime> GetDateTimes(int page, DateTime inputDate)
        {
            if (IsMock) return new List<DateTime>();

            Connect();
            var command = new SqlCommand(@"SELECT DateTime FROM Entry 
                WHERE DATEPART(yy, DateTime) = @year 
                AND DATEPART(mm, DateTime) = @month 
                AND DATEPART(dd, DateTime) = @day 
                ORDER BY id 
                OFFSET (@skip) ROWS 
                FETCH NEXT 8 ROWS ONLY", Connection);
            command.Parameters.AddWithValue("@skip", (page - 1) * 8);
            command.Parameters.AddWithValue("@year", inputDate.Year);
            command.Parameters.AddWithValue("@month", inputDate.Month);
            command.Parameters.AddWithValue("@day", inputDate.Day);
            var dateTime = new List<DateTime>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dateTime.Add(Convert.ToDateTime($"{reader[0]}"));
                }
            }
            Disconnect();
            return dateTime;
        }

        /// <summary>
        /// Returns a list of datetimes for a given datetime (including time)
        /// </summary>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public List<DateTime> GetDateTimes(DateTime inputDate)
        {
            if (IsMock) return new List<DateTime>();

            Connect();
            var command = new SqlCommand(@"SELECT DateTime FROM Entry 
                WHERE DATEPART(yy, DateTime) = @year 
                AND DATEPART(mm, DateTime) = @month 
                AND DATEPART(dd, DateTime) = @day 
                ORDER BY id", Connection);
            command.Parameters.AddWithValue("@year", inputDate.Year);
            command.Parameters.AddWithValue("@month", inputDate.Month);
            command.Parameters.AddWithValue("@day", inputDate.Day);
            var dateTime = new List<DateTime>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dateTime.Add(Convert.ToDateTime($"{reader[0]}"));
                }
            }
            Disconnect();
            return dateTime;
        }
    }
}
