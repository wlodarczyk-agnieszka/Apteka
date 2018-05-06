using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Apteka
{
    static class Show
    {
        private static SqlConnection _connection = new SqlConnection("Initial Catalog=Apteka;" +
                                                                "Data Source=192.168.29.128;" +
                                                                "User id=sa;" + "Password=Test2010;");
        private static SqlCommand _command = new SqlCommand();

        public static void ShowMedicines(string query)
        {
            //_command.CommandText = "SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines;";
            _command.CommandText = query;
            _connection.Open();

            _command.Connection = _connection;
            var reader = _command.ExecuteReader();

           

            if (reader.HasRows)
            {
                string id = "ID".PadRight(4);
                string name = "Name".PadRight(20);
                string price = "Price".PadRight(7);
                string amount = "Amount".PadRight(7);
                string with = "With".PadRight(5);
                string manufact = "Manufacturer".PadRight(20);

                string label = $"{id} | {name} | {price} | {amount} | {with} | {manufact}";

                Console.WriteLine();
                Console.WriteLine(label);
                Console.WriteLine("----------------------------------------------------------------------");

                while (reader.Read())
                {
                    string nm = reader["Name"].ToString();
                    if (nm.Length >= 20)
                    {
                        nm = nm.Substring(0, 17) + "...";
                    }

                    Console.WriteLine($"{reader["ID"].ToString().PadRight(4)} | {nm.PadRight(20)} | {reader["Price"].ToString().PadRight(7)} " +
                                      $"| {reader["Amount"].ToString().PadRight(7)} | {reader["WithPrescription"].ToString().PadRight(5)} | {reader["Manufacturer"].ToString().PadRight(20)}");
                }

                Console.WriteLine();
            }
            _connection.Close();

        }
    }
}
