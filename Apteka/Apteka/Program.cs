using System;
using System.Reflection.Metadata.Ecma335;

namespace Apteka
{
    class Program
    {
        static void Main(string[] args)
        {

            int option;
            while (true)
            {
                //Console.WriteLine("\nMENU");
                Console.WriteLine("\n*** MAGAZYN ***");
                Console.WriteLine("1. Dodaj Lek");
                Console.WriteLine("2. Pokaż listę leków");
                Console.WriteLine("3. Aktualizuj lek");
                Console.WriteLine("4. Usuń lek");
                Console.WriteLine("*** SPRZEDAŻ ***");
                Console.WriteLine("5. Sprzedaj lek");
                Console.WriteLine("*** PACJENCI ***");
                Console.WriteLine("6. Lista pacjentów i recept");
                Console.WriteLine("*** RAPORTY ***");
                Console.WriteLine("7. Leki - mały stan magazynowy");
                Console.WriteLine("8. Sprzedaż - Top 10 leków");
                Console.WriteLine("9. Sprzedaż - Top 10 pacjentów");
                Console.WriteLine("*** INNE ***");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierasz: ");
                option = Convert.ToInt32(Console.ReadLine());

                if (option == 0) break;

                switch (option)
                {
                    case 1:
                        Get.AddMedicine();
                        break;
                    case 2:
                        Show.ShowMedicines("SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines;");
                        break;
                    case 3:
                        Get.UpdateMedicine();
                        break;
                    case 4:
                        Get.DeleteMedicine();
                        break;
                    case 5:
                        Sell2();
                        break;
                    case 7:
                        Show.ShowMedicines("SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines WHERE Amount <= 20 ORDER BY Amount ASC;");
                        break;
                    default:
                        continue;

                }
            }

            //Console.ReadKey();
        }

        static void Sell()
        {
            Console.Write("Podaj ID leku: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj ilość: ");
            int amount = Convert.ToInt32(Console.ReadLine());

            Sales.SellMedicine(id, amount);

        }

        static void Sell2()
        {
            Console.Write("Podaj ID leku: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj ilość: ");
            int amount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj numer recepty: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.Write("Imię inazwisko pacjenta: ");
            string customer = Console.ReadLine();

            Console.Write("PESEL pacjenta: ");
            string pesel = Console.ReadLine();

            Sales.SellMedicine(id, amount, number,customer, pesel);

        }


    }
}
