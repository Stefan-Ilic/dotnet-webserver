using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
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
        /// Adds a Temperature and its DateTime to the database every minute
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="dateTime"></param>
        public void AddEntry(float temperature, DateTime dateTime)
        {
            System.Threading.Thread.Sleep(60 * 1000);

            Connect();

            var command = new SqlCommand(@"INSERT INTO Entry (Temperature, DateTime)
                VALUES (@temp, @datetime)", Connection);

            command.Parameters.AddWithValue("@temp", temperature);
            command.Parameters.AddWithValue("@datetime", dateTime);
            command.ExecuteNonQuery();

            Disconnect();
        }

        //TODO add xml
        public List<float> GetAllTemps()
        {
            Connect();
            var command = new SqlCommand(@"SELECT Temperature FROM Entry", Connection);
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

        //TODO add xml
        public List<DateTime> GetAllDateTimes()
        {
            Connect();
            var command = new SqlCommand(@"SELECT DateTime FROM Entry", Connection);
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
