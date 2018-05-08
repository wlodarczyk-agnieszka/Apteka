using System;
using System.Collections.Generic;
using System.Data;
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
                string name = "Lek".PadRight(20);
                string price = "Cena".PadRight(7);
                string amount = "Ilość".PadRight(7);
                string with = "Rec.?".PadRight(5);
                string manufact = "Producent".PadRight(20);

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

        public static void ShowPrescriptionsAndPatients()
        {
            _command.CommandText = "select p.PrescriptionNumber, p.CustomerName, p.PESEL, m.Name, o.Amount, o.Date " +
                                   "from Medicines m inner join Orders o on m.ID = o.MedicineID " +
                                   "inner join Prescriptions p on p.ID = o.PrescriptionID";
            _command.Connection = _connection;
            _connection.Open();

            var reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                string prescNumber = "Recepta".PadRight(8);
                string custName = "Klient".PadRight(15);
                string pesel = "PESEL".PadRight(11);
                string amount = "Ilość".PadRight(5);
                string medName = "Lek".PadRight(15);
                string date = "Data".PadRight(10);

                string label = $"{prescNumber} | {custName} | {pesel} | {medName} | {amount} | {date}";

                Console.WriteLine();
                Console.WriteLine(label);
                Console.WriteLine("---------------------------------------------------------------------------");

                while (reader.Read())
                {
                    string medNm = reader["Name"].ToString();
                    if (medNm.Length >= 15)
                    {
                        medNm = medNm.Substring(0, 12) + "...";
                    }

                    string custNm = reader["CustomerName"].ToString();
                    if (custNm.Length >= 15)
                    {
                        custNm = custNm.Substring(0, 12) + "...";
                    }

                    //DateTime sellDate = Convert.ToDateTime(reader["Date"]).ToShortDateString();
                    

                    Console.WriteLine($"{reader["PrescriptionNumber"].ToString().PadRight(8)} | {custNm.PadRight(15)} | {reader["PESEL"].ToString().PadRight(11)} " +
                                      $"| {medNm.PadRight(15)} | {reader["Amount"].ToString().PadRight(5)} | {reader["Date"].ToString().Substring(0, 10).PadRight(10)}");
                }

                Console.WriteLine();
            }
            _connection.Close();
        }

        public static void ShowMedicineBestsellers()
        {
            _command.CommandText = "select top 10 m.Name, sum(o.Amount) as Sold from Medicines m inner join Orders o on m.ID = o.MedicineID group by m.Name order by Sold desc;";
            _command.Connection = _connection;
            _connection.Open();

            var reader = _command.ExecuteReader();

            if (reader.HasRows)
            {
                string medName = "Lek".PadRight(20);
                string sold = "Sprzedanych".PadRight(5);
                string nr = "Nr".PadRight(2);

                string label = $"{nr} | {medName} | {sold}";

                Console.WriteLine();
                Console.WriteLine(label);
                Console.WriteLine("---------------------------------------");

                int i = 1;

                while (reader.Read())
                {
                    string nm = reader["Name"].ToString();
                    if (nm.Length >= 20)
                    {
                        nm = nm.Substring(0, 17) + "...";
                    }

                    Console.WriteLine($"{i.ToString().PadRight(2)} | {nm.PadRight(20)} | {reader["Sold"].ToString().PadRight(5)}");
                    i++;
                }

                Console.WriteLine();
            }
            _connection.Close();
        }

        public static void SearchMedicines(string searchWord)
        {
            _command.Parameters.Clear();
            _command.CommandText = "SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines WHERE Name like @search;";
            _command.Parameters.Add(new SqlParameter("@search", SqlDbType.NVarChar) { Value = "%" + searchWord + "%" });
            _command.Connection = _connection;
            _connection.Open();

            _command.Connection = _connection;
            var reader = _command.ExecuteReader();



            if (reader.HasRows)
            {
                string id = "ID".PadRight(4);
                string name = "Lek".PadRight(20);
                string price = "Cena".PadRight(7);
                string amount = "Ilość".PadRight(7);
                string with = "Rec.?".PadRight(5);
                string manufact = "Producent".PadRight(20);

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
            else
            {
                Console.WriteLine("Brak wyników.");
            }
            _connection.Close();

        }
    }
}
