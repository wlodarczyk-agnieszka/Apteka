using System;
using System.Data;
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
                Console.WriteLine("Brak produktu z podanym ID w bazie.");
                _connection.Close();
                return null;
            }

        }
      
    }
}
