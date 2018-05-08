using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Apteka
{
    public static class Sales
    {
        private static SqlConnection _connection = new SqlConnection("Initial Catalog=Apteka;" +
                                                                     "Data Source=192.168.29.128;" +
                                                                     "User id=sa;" + "Password=Test2010;");
        private static SqlCommand _command = new SqlCommand();

        public static void Sell()
        {
            Console.Write("Podaj ID leku: ");
            int medicineID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj ilość: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Sprawdzam dostępność...");

            // sprawdzamy czy lek istnieje, czy jest wystarczająca jego ilosc i czy jest na recepte
            _command.Parameters.Clear();
            _command.CommandText = "SELECT ID, Amount, WithPrescription FROM Medicines WHERE ID = @id;";
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = medicineID });
            _command.Connection = _connection;

            _connection.Open();
            var reader = _command.ExecuteReader();
            

            if (reader.Read())
            {
                if (Convert.ToInt32(reader["Amount"]) < amount)
                {
                    Console.WriteLine($"Nie ma wystarczającej ilości leku w magazynie (potrzeba: {amount}, posiadamy: {reader["Amount"]}).");
                    _connection.Close();
                }
                else
                {
                    if (Convert.ToByte(reader["WithPrescription"]) == 1)
                    {
                        Console.WriteLine("Lek na receptę. Podaj dane:");
                        Console.Write("Numer recepty: ");
                        int number = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Imię i nazwisko pacjenta: ");
                        string customer = Console.ReadLine();

                        Console.Write("PESEL pacjenta: ");
                        string pesel = Console.ReadLine();

                        _connection.Close();

                        SellMedicineWithPrescription(medicineID, amount, number,customer, pesel);
                    }
                    else
                    {
                        _connection.Close();

                        SellMedicine(medicineID, amount);
                    }

                    Console.WriteLine($"Sprzedano {amount} sztuk(i) leku.");
                }
            }
            else
            {
                Console.WriteLine("Lek o podanym ID nie istnieje.");
                _connection.Close();
            }

            //_connection.Close();
        }
        

        private static void SellMedicine(int medicineID, int amount) 
        {
            // wersja bez recepty
            _command.Parameters.Clear();
            _command.CommandText = "UPDATE Medicines SET Amount = Amount - @amount WHERE ID = @id; " +
                                   "INSERT INTO Orders (MedicineID, Amount) VALUES (@id, @amount);";
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = medicineID });
            _command.Parameters.Add(new SqlParameter("@amount", SqlDbType.Int) { Value = amount });
            _command.Connection = _connection;

            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();

        }

        private static void SellMedicineWithPrescription(int medicineID, int amount, int prescriptionNumber, string customerName, string pesel)
        {
            // wersja z recepta
            _command.Parameters.Clear();
            _command.CommandText = "UPDATE Medicines SET Amount = Amount - @amount WHERE ID = @id; " +
                                   "INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES (@customer, @pesel, @prescription);" +
                                   "INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (@id, SCOPE_IDENTITY(), @amount);";
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = medicineID });
            _command.Parameters.Add(new SqlParameter("@amount", SqlDbType.Int) { Value = amount });
            _command.Parameters.Add(new SqlParameter("@prescription", SqlDbType.Int) { Value = prescriptionNumber });
            _command.Parameters.Add(new SqlParameter("@customer", SqlDbType.NVarChar) { Value = customerName });
            _command.Parameters.Add(new SqlParameter("@pesel", SqlDbType.NVarChar) { Value = pesel });
            _command.Connection = _connection;

            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();

            
        }

    }
}
