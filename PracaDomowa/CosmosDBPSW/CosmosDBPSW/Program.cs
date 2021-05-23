using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDBPSW
{
    class Program
    {
        public static async Task Main(string[] args)
        {


            int quit = 0;
            while (quit == 0)
            {

                Console.WriteLine("wybierz akcje");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1 - Edytuj pracowników");
                Console.WriteLine("2 - Edytuj zwierzęta");
                Console.WriteLine("3 - Quit");


                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        await EditEmployees();
                        break;
                    case 2:
                        await EditAnimal();
                        break;
                    case 3:
                        quit = 1;
                        Console.WriteLine("Wyjscie");
                        break;
                    default:
                        Console.WriteLine("zły numer");
                        break;
                }
            }

            return;

        }

        public static async Task EditEmployees()
        {
            CloudTable table = await CreateTableAsync("employees");
            int quit = 0;
            while (quit == 0)
            {

                Console.WriteLine("wybierz akcje");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1 - Wyswietl wszystko");
                Console.WriteLine("2 - Daj podwyżke poniżej kwoty");
                Console.WriteLine("3 - Dodaj");
                Console.WriteLine("4 - edytuj");
                Console.WriteLine("5 - usun");
                Console.WriteLine("6 - Quit");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        Console.WriteLine("wszyscy: ");
                        IQueryable<Employee> map = table.CreateQuery<Employee>().Where(x => x.PartitionKey == "employees"  ).Select(x => new Employee
                        {
                            RowKey = x.RowKey,
                            Name = x.Name,
                            BirthYear = x.BirthYear,
                            Specialization = x.Specialization,
                            Salary = x.Salary
                        });
                        foreach (var e in  map)
                        {
                            Console.WriteLine(e.RowKey + " => " + e.ToString());
                        }
                        break;
                    case "2":
                        Console.WriteLine("podaj kwote");
                        double salary4 = double.Parse(Console.ReadLine());
                        Console.WriteLine("podaj nową kwote");
                        double salary5 = double.Parse(Console.ReadLine());
                        List<Employee> map2 = table.CreateQuery<Employee>().Where(x => x.PartitionKey == "employees" && x.Salary < salary4).Select(x => new Employee
                        {
                            RowKey = x.RowKey,
                            Name = x.Name,
                            BirthYear = x.BirthYear,
                            Specialization = x.Specialization,
                            Salary = x.Salary
                        }).ToList();
                        foreach (var e in map2)
                        {
                            e.Salary = salary5;
                            TableOperation editOperation3 = TableOperation.InsertOrReplace(e);
                            await table.ExecuteAsync(editOperation3);
                        }

                        break;
                    case "3":
                        Guid key1 = Guid.NewGuid();
                        Console.WriteLine("podaj imie ");
                        string name = Console.ReadLine();
                        Console.WriteLine("rok urodzenia");
                        int birthYear = int.Parse(Console.ReadLine());
                        Console.WriteLine("specjalizacja");
                        string specialization = Console.ReadLine();
                        Console.WriteLine("pensja");
                        double salary = double.Parse(Console.ReadLine());
                        Employee employee1 = new Employee()
                        {
                            RowKey = key1.ToString(),
                            Name = name,
                            BirthYear = birthYear,
                            Specialization = specialization,
                            Salary= salary
                        }; 
                        Console.WriteLine("PUT " + key1 + " => " + employee1);

                        TableOperation insertOperation = TableOperation.InsertOrMerge(employee1);
                         await table.ExecuteAsync(insertOperation);

                        break;
                    case "4":
                        Console.WriteLine("wpisz id do edycji");
                        string idToEdit = Console.ReadLine();
                        Employee objectToEdit = table.CreateQuery<Employee>().Where(x => x.RowKey == idToEdit).FirstOrDefault();
                        int endEdit = 0;
                        while (endEdit == 0)
                        {

                            Console.WriteLine("co chcesz edytować");
                            Console.WriteLine("-------------------------");
                            Console.WriteLine("1 - imie");
                            Console.WriteLine("2 - rok urodzenia");
                            Console.WriteLine("3 - specjalizacja");
                            Console.WriteLine("4 - pensja");
                            Console.WriteLine("5 - zakończ edycje");

                            string choice2 = Console.ReadLine();

                            switch (choice2)
                            {
                                case "1":

                                    Console.WriteLine("podaj nowe imie");
                                    string name2 = Console.ReadLine();
                                    objectToEdit.Name = name2;
                                    break;
                                case "2":
                                    Console.WriteLine("rok urodzenia");
                                    int birthYear2 = int.Parse(Console.ReadLine());
                                    objectToEdit.BirthYear = birthYear2;
                                    break;
                                case "3":
                                    Console.WriteLine("specjalizacja");
                                    string specialization2 = Console.ReadLine();
                                    objectToEdit.Specialization = specialization2;
                                    break;
                                case "4":
                                    Console.WriteLine("pensja");
                                    double salary1 = double.Parse(Console.ReadLine());
                                    objectToEdit.Salary = salary1;
                                    break;
                                case "5":
                                    endEdit = 1;
                                    break;
                                default:
                                    Console.WriteLine("zły numer");
                                    break;
                            }

                        }

                        TableOperation editOperation = TableOperation.InsertOrReplace(objectToEdit);
                        await table.ExecuteAsync(editOperation);

                        break;
                    case "5":

                        Console.WriteLine("wpisz id do usuniecia");
                        string id1 = Console.ReadLine();

                        Employee objectTodelete = table.CreateQuery<Employee>().Where(x => x.RowKey == id1).FirstOrDefault();
                        TableOperation deleteOperation = TableOperation.Delete(objectTodelete);
                        TableResult deleteResult = await table.ExecuteAsync(deleteOperation);
                        break;
                    case "6":
                        quit = 1;
                        Console.WriteLine("Wyjscie");
                        break;
                    default:
                        Console.WriteLine("zły numer");
                        break;
                }
            }
            return;
        }
        static async Task EditAnimal()
        {
            CloudTable table = await CreateTableAsync("animals");
            int quit = 0;
            while (quit == 0)
            {

                Console.WriteLine("wybierz akcje");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1 - Wyswietl wszystko");
                Console.WriteLine("2 - Wyswietl daną rase albo gatunek");
                Console.WriteLine("3 - Dodaj");
                Console.WriteLine("4 - Edytuj");
                Console.WriteLine("5 - Usun");
                Console.WriteLine("6 - Quit");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        Console.WriteLine("All animals: ");
                        IQueryable<Animal> map = table.CreateQuery<Animal>().Where(x => x.PartitionKey == "animals").Select(x => new Animal
                        {
                            RowKey = x.RowKey,
                            Name = x.Name,
                            BirthYear = x.BirthYear,
                            Species = x.Species,
                            Breed = x.Breed,
                            Catwalk = x.Catwalk
                        });
                        foreach (var e in map)
                        {
                            Console.WriteLine(e.RowKey + " => " + e.ToString());
                        }
                        break;
                    case "2":
                        int endsee = 0;
                        while (endsee == 0)
                        {

                            Console.WriteLine("co chcesz wyświetlic");
                            Console.WriteLine("-------------------------");
                            Console.WriteLine("1 - rase");
                            Console.WriteLine("2 - gatunek");
                            Console.WriteLine("3 - koniec");
                            string choice2 = Console.ReadLine();
                            IQueryable<Animal> map1 = table.CreateQuery<Animal>().Where(x => x.PartitionKey == "animals").Select(x => new Animal
                            {
                                RowKey = x.RowKey,
                                Name = x.Name,
                                BirthYear = x.BirthYear,
                                Species = x.Species,
                                Breed = x.Breed,
                                Catwalk = x.Catwalk
                            });
                            switch (choice2)
                            {
                                case "1":

                                    Console.WriteLine("podaj rase");
                                    string name22 = Console.ReadLine();
                                    foreach (var e in  map1)
                                    {
                                        if (e.Breed == name22)
                                        {
                                            Console.WriteLine(e.PartitionKey + " => " + e.ToString());
                                        }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("podaj gatunek");
                                    string name23 = Console.ReadLine();
                                    foreach (var e in  map1)
                                    {
                                        if (e.Species == name23)
                                        {
                                            Console.WriteLine(e.PartitionKey + " => " + e.ToString());
                                        }
                                    }
                                    break;
                                case "3":
                                    endsee = 1;
                                    break;
                                default:
                                    Console.WriteLine("zły numer");
                                    break;
                            }

                        }

                        break;
                    case "3":
                        Guid key1 = Guid.NewGuid();
                        Console.WriteLine("podaj imie ");
                        string name = Console.ReadLine();
                        Console.WriteLine("rok urodzenia");
                        int birthYear = int.Parse(Console.ReadLine());
                        Console.WriteLine("uwagi");
                        string comments = Console.ReadLine();
                        Console.WriteLine("numer wybiegu");
                        int catwalk = int.Parse(Console.ReadLine());

                        Console.WriteLine("wpisz gatunek ");
                        string species = Console.ReadLine();

                        Console.WriteLine("wpisz rase");
                        string breed = Console.ReadLine();

                        Animal animal1 = new Animal()
                        {
                            Name = name,
                            BirthYear = birthYear,
                            Comments =comments,
                            Catwalk = catwalk,
                            Breed = breed,
                            Species = species
                        };
                        Console.WriteLine("PUT " + key1 + " => " + animal1);

                        TableOperation insertOperation = TableOperation.InsertOrMerge(animal1);
                        TableResult result = await table.ExecuteAsync(insertOperation);

                        break;
                    case "4":
                        Console.WriteLine("wpisz id do edycji");
                        string idToEdit = Console.ReadLine();
                        Animal objectToEdit = table.CreateQuery<Animal>().Where(x => x.RowKey == idToEdit).FirstOrDefault();

                        int endEdit = 0;
                        while (endEdit == 0)
                        {

                            Console.WriteLine("co chcesz edytować");
                            Console.WriteLine("-------------------------");
                            Console.WriteLine("1 - imie");
                            Console.WriteLine("2 - rok urodzenia");
                            Console.WriteLine("3 - uwagi");
                            Console.WriteLine("4 - numer wybiegu");
                            Console.WriteLine("5 - rasa");
                            Console.WriteLine("6 - gatunek");
                            Console.WriteLine("7 - zakończ edycje");

                            string choice2 = Console.ReadLine();

                            switch (choice2)
                            {
                                case "1":

                                    Console.WriteLine("podaj nowe imie");
                                    string name2 = Console.ReadLine();
                                    objectToEdit.Name = name2;
                                    break;
                                case "2":
                                    Console.WriteLine("rok urodzenia");
                                    int birthYear2 = int.Parse(Console.ReadLine());
                                    objectToEdit.BirthYear = birthYear2;
                                    break;
                                case "3":
                                    Console.WriteLine("uwagi");
                                    string comments2 = Console.ReadLine();
                                    objectToEdit.Comments = comments2;
                                    break;
                                case "4":
                                    Console.WriteLine("numer wybiegu");
                                    int catwalk2 = int.Parse(Console.ReadLine());
                                    objectToEdit.Catwalk = catwalk2;
                                    break;
                                case "5":
                                    Console.WriteLine("wpisz gatunek ");

                                    string species2 = Console.ReadLine();
                                    objectToEdit.Species = species2;
                                    break;
                                case "6":
                                    Console.WriteLine("wpisz rase");

                                    string breed2 = Console.ReadLine();
                                    objectToEdit.Breed = breed2;
                                    break;
                                case "7":
                                    endEdit = 1;
                                    break;
                                default:
                                    Console.WriteLine("zły numer");
                                    break;
                            }

                        }

                        TableOperation editOperation = TableOperation.InsertOrReplace(objectToEdit);
                        await table.ExecuteAsync(editOperation);

                        break;
                    case "5":

                        Console.WriteLine("wpisz id do usuniecia");
                        string id1 = Console.ReadLine();
                        Animal objectdelete = table.CreateQuery<Animal>().Where(x => x.RowKey == id1).FirstOrDefault();
                        TableOperation deleteOperation = TableOperation.Delete(objectdelete);
                        TableResult deleteResult = await table.ExecuteAsync(deleteOperation);
                        break;
                    case "6":
                        quit = 1;
                        Console.WriteLine("Wyjscie");
                        break;
                    default:
                        Console.WriteLine("zły numer");
                        break;
                }
            }
            return;
        }

        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }

            Console.WriteLine();
            return table;
        }
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}
