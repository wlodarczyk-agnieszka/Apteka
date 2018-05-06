using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Apteka
{
    static class Get
    {
        private static SqlConnection _connection = new SqlConnection("Initial Catalog=Apteka;" +
                                                                     "Data Source=192.168.29.128;" +
                                                                     "User id=sa;" + "Password=Test2010;");
        private static SqlCommand _command = new SqlCommand();


        public static Medicines GetMedicine(int id)
        {
            // zwraca obiekt typu Medicines o podanym ID
            _command.Parameters.Clear();
            _command.CommandText = "SELECT ID, Name, Price, Manufacturer, Amount, WithPrescription FROM Medicines WHERE ID = @id;";
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            _command.Connection = _connection;

            _connection.Open();
            var reader = _command.ExecuteReader();

            if (reader.Read())
            {
                var m = new Medicines(reader["Name"].ToString(), reader["Manufacturer"].ToString(), Convert.ToDecimal(reader["Price"]), Convert.ToInt32(reader["Amount"]), Convert.ToByte(reader["WithPrescription"]), Convert.ToInt32(reader["ID"]));

                _connection.Close();
                return m;
            }
            else
            {
                //throw new Exception("Brak produktu z podanym ID w bazie.");
                Console.WriteLine("Brak produktu z podanym ID w bazie.");
                _connection.Close();
                return null;
            }

            //_connection.Close();

        }

        public static void AddMedicine()
        {
            Console.Write("Nazwa leku: ");
            string name = Console.ReadLine();

            Console.Write("Nazwa producenta: ");
            string prod = Console.ReadLine();

            decimal price = 0;
            int amount = 0;
            byte with = 0;

            try
            {
                Console.Write("Cena: ");
                price = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Ilość: ");
                amount = Convert.ToInt32(Console.ReadLine());

                Console.Write("Na receptę? (0/1): ");
                with = Convert.ToByte(Console.ReadLine());

                if (with != 0)
                {
                    with = 1;
                }

                var m = new Medicines(name, prod, price, amount, with);
                m.Save();

            }
            catch (FormatException)
            {
                Console.WriteLine("Błędny format liczby.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd: " + e);
            }
        }

        public static void UpdateMedicine()
        {
            Console.Write("Podaj ID leku: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var m = Get.GetMedicine(id);

            Console.WriteLine($"Nazwa: {m.Name}");
            Console.Write("Nowa nazwa: ");
            string newName = Console.ReadLine();
            if (newName != "")
            {
                m.Name = newName;
            }

            try
            {
                Console.WriteLine($"Cena: {m.Price}");
                Console.Write("Nowa cena: ");
                string newPrice = Console.ReadLine();
                if (newPrice != "")
                {
                    m.Price = Convert.ToInt32(newPrice);
                }

                Console.WriteLine($"Ilość: {m.Amount}");
                Console.Write("Nowa ilość: ");
                string newAmount = Console.ReadLine();
                if (newAmount != "")
                {
                    m.Amount = Convert.ToInt32(newAmount);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Błędny format liczby.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd: " + e);
            }



            m.Save();
        }

        public static void DeleteMedicine()
        {
            Console.Write("Podaj ID leku: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var m = Get.GetMedicine(id);

            m?.Remove();
        }
    }
}
