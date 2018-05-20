using System;
using System.Data;
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
        public decimal Price
        {
            get => _price;
            set { _price = value > 0 ? value : 0; }
        }

        public int Amount
        {
            get => _amount;
            set { _amount = value > 0 ? value : 0; }
        }
        public byte WithPrescription
        {
            get => _withPrescription;
            set => _withPrescription = value;
        }


        public Medicines(string name, string manufacturer, decimal price, int amount, byte withPrescription = 0, int id = 0)
        {
            ID = id;
            _name = name;
            _manufacturer = manufacturer;
            _price = price;
            _amount = amount;
            _withPrescription = withPrescription != 0 ? (byte) 1 : (byte) 0;
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
            _command.Parameters.Clear();
            _command.CommandText = "SELECT Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines WHERE ID = @id;";

            _command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = ID });
            _command.Connection = _connection;
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
                if (Console.ReadLine() != null)
                {
                    price = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
                }
                

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

            if (m != null)
            {
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
                        m.Price = Convert.ToDecimal(newPrice.Replace('.', ','));
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
            else
            {
                Console.WriteLine("Brak leku o podanym ID.");
            }
            

            
        }

        public static void DeleteMedicine()
        {
            Console.Write("Podaj ID leku: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var m = Get.GetMedicine(id);

            if (m != null)
            {
                m.Remove();
                Console.WriteLine("Usunięto.");
            }
            else
            {
                Console.WriteLine("Brak leku o podanym ID.");
            }


        }

    }
}

