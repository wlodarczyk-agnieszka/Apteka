using System;
using System.Reflection.Metadata.Ecma335;

namespace Apteka
{
    class Program
    {
        static void Main(string[] args)
        {

            int option = -1;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n*** MAGAZYN ***");
                Console.WriteLine("0. Szukaj leku");
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
                Console.WriteLine("10. Wyjście");
                Console.Write("Wybierasz: ");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Błędna opcja.");
                }

                if (option == 10) break;

                switch (option)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("Szukana nazwa: ");
                        string searchName = Console.ReadLine();
                        Show.SearchMedicines(searchName);
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Medicines.AddMedicine(); //.AddMedicine();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Show.ShowMedicines("SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines;");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Medicines.UpdateMedicine();//.UpdateMedicine();
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Medicines.DeleteMedicine();//.DeleteMedicine();
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Sales.Sell();
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Show.ShowPrescriptionsAndPatients();
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Show.ShowMedicines("SELECT ID, Name, Manufacturer, Price, Amount, WithPrescription FROM Medicines WHERE Amount <= 20 ORDER BY Amount ASC;");
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Show.ShowMedicineBestsellers();
                        break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Show.ShowTopBuyers();
                        break;
                    default:
                        continue;

                }
            }
        }
    }
    /* TODO:
     * ustawianie cen i ilosci - nie przyjmuje ujemnych
     * po wprowadzeniu leku z recepty - pytanie czy wprowadzic kolejny lek na ta sama recepte
     * przeniesc metody AddMedicine itp jako statyczne do klasy Medicines
     * refaktoring, usuniecie powtorzen, skrocenie/ rozbicie metod
     * na nowo tabele, zaladowac wieksza ilosc danych
     */
}
