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

        

        public static void SellMedicine(int medicineID, int amount) //zakladam, ze podane ID leku jest poprawne
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

        public static void SellMedicine(int medicineID, int amount, int prescriptionNumber, string customerName, string pesel)
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
