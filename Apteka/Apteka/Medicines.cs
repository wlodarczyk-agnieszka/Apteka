using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Apteka
{
    class Medicines : ActiveRecord
    {

        private string _name;
        private string _manufacturer;
        private decimal _price;
        private int _amount;
        private byte _withPrescription;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value;
        }
        public decimal Price { get => _price;
            set => _price = value;
        }
        public int Amount { get => _amount;
            set => _amount = value;
        }
        public byte WithPrescription { get => _withPrescription;
            set => _withPrescription = value;
        }


        public Medicines(string name, string manufacturer, decimal price, int amount, byte withPrescription = 0, int id = 0)
        {
            ID = id;
            _name = name;
            _manufacturer = manufacturer;
            _price = price;
            _amount = amount;
            if (withPrescription != 0)
            {
                _withPrescription = 1;
            }
            else
            {
                _withPrescription = 0;
            }
        }



        public override void Save()
        {
            _command.Parameters.Clear();

            _command.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar) { Value = Name });
            _command.Parameters.Add(new SqlParameter("@manufacturer", SqlDbType.VarChar) { Value = _manufacturer });
            _command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal) { Value = _price });
            _command.Parameters.Add(new SqlParameter("@amount", SqlDbType.Int) { Value = _amount });
            _command.Parameters.Add(new SqlParameter("@withPrescription", SqlDbType.Bit) { Value = _withPrescription });
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = ID });

            if (ID == 0)
            {
                // insert
                _command.CommandText = "INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription)" +
                                       "VALUES (@name, @manufacturer, @price, @amount, @withPrescription); SELECT SCOPE_IDENTITY();";

                Open();
                ID = Convert.ToInt32(_command.ExecuteScalar());
                Close();

                Console.WriteLine($"Inserted, ID = {ID}");

            }
            else
            {
                // update
                _command.CommandText = "UPDATE Medicines SET Name = @name, Manufacturer = @manufacturer, Price = @price, " +
                                       "Amount = @amount, WithPrescription = @withPrescription WHERE ID = @id;";

                Open();
                _command.ExecuteNonQuery();
                Close();

                Console.WriteLine($"Updated, ID = {ID}");
            }
        }

        public override void Reload()
        {

            _command.CommandText = "SELECT Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines WHERE ID = @id;";

            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = ID });

            _connection.Open();

            var reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                Name = reader["Name"].ToString();
                _manufacturer = reader["Manufacturer"].ToString();
                _price = Convert.ToDecimal(reader["Price"]);
                _amount = Convert.ToInt32(reader["Amount"]);
                _withPrescription = Convert.ToByte(reader["WithPrescription"]);
            }
            else
            {
                Console.WriteLine($"Brak produktu o podanym ID! ( {ID} )");
            }

            _connection.Close();
        }

        public override void Remove()
        {
            _command.Parameters.Clear();
            _command.CommandText = "DELETE FROM Medicines WHERE ID = @id;";
            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = ID });


            Open();

            _command.ExecuteNonQuery();

            Close();
        }

    }
}

