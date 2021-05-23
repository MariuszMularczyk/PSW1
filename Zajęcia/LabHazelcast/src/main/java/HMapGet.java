import com.hazelcast.client.config.ClientConfig;

import java.net.UnknownHostException;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Random;
import java.util.Scanner;

import com.hazelcast.client.HazelcastClient;
import com.hazelcast.core.Hazelcast;
import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.map.IMap;

public class HMapGet {
    final private static Random r = new Random(System.currentTimeMillis());
    public static void main( String[] args ) throws UnknownHostException {
        ClientConfig clientConfig = HConfig.getClientConfig();
        HazelcastInstance client = HazelcastClient.newHazelcastClient( clientConfig );
        int quit = 0;
        while (quit == 0)
        {

            System.out.println("wybierz akcje");
            System.out.println("-------------------------");
            System.out.println("1 - Edytuj pracowników");
            System.out.println("2 - Edytuj zwierzęta");
            System.out.println("3 - Quit");

            Scanner scanner = new Scanner(System.in);
            int choice = scanner.nextInt();
            switch (choice)
            {
                case 1:
                     EditEmployees(client);
                    break;
                case 2:
                     EditAnimal(client);
                    break;
                case 3:
                    quit = 1;
                    System.out.println("Wyjscie");
                    break;
                default:
                    System.out.println("zły numer");
                    break;
            }
        }
        client.shutdown();
    }

    public static void EditEmployees(HazelcastInstance instance)  {
        int quit = 0;
        while(quit == 0){

            System.out.println("Choose from these choices");
            System.out.println("-------------------------\n");
            System.out.println("1 - Wyswietl wszystko");
            System.out.println("2 - podwyżka zarabiających poniżej kwoty");
            System.out.println("3 - Dodaj");
            System.out.println("4 - edytuj");
            System.out.println("5 - usun");
            System.out.println("6 - Quit");

            Scanner scanner = new Scanner(System.in);
            int choice = scanner.nextInt();
            IMap<Long, Employee> map = instance.getMap( "empolyees" );
            switch (choice) {
                case 1:

                    System.out.println("All employees: ");
                    for(Map.Entry<Long, Employee> e : map.entrySet()){
                        System.out.println(e.getKey() + " => " + e.getValue());
                    }
                    break;
                case 2:

                    System.out.println("podaj kwote");
                    float salary4 = scanner.nextFloat();

                    System.out.println("podaj nową kwote");
                    float salary5 = scanner.nextFloat();

                    for(Map.Entry<Long, Employee> e : map.entrySet()){
                        if( e.getValue().getSalary() < salary4)
                        {

                            System.out.println(e.getKey() + " => " + e.getValue());
                            Employee objectToEdit = e.getValue();
                            objectToEdit.setSalary(salary5);
                            map.replace(e.getKey(),objectToEdit);
                        }
                    }
                    break;
                case 3:
                    Long key1 = (long) Math.abs(r.nextInt());
                    System.out.println("podaj imie ");
                    scanner.nextLine();
                    String name = scanner.nextLine();
                    System.out.println("rok urodzenia");
                    int birthYear = scanner.nextInt();
                    System.out.println("specjalizacja");
                    scanner.nextLine();
                    String specialization = scanner.nextLine();
                    System.out.println("pensja");
                    float salary = scanner.nextFloat();
                    Employee employee1 = new Employee(name, birthYear, specialization, salary);
                    System.out.println("PUT " + key1 + " => " + employee1);
                    map.put(key1, employee1);
                    break;
                case 4:
                    System.out.println("wpisz id do edycji");
                    long idToEdit =  scanner.nextLong();;
                    Employee objectToEdit = map.get(idToEdit);
                    int endEdit = 0;
                    while (endEdit == 0)
                    {

                        System.out.println("co chcesz edytować");
                        System.out.println("-------------------------");
                        System.out.println("1 - imie");
                        System.out.println("2 - rok urodzenia");
                        System.out.println("3 - specjalizacja");
                        System.out.println("4 - pensja");
                        System.out.println("5 - zakończ edycje");

                        int choice2 = scanner.nextInt();

                        switch (choice2)
                        {
                            case 1:

                                System.out.println("podaj nowe imie");
                                scanner.nextLine();
                                String name2 = scanner.nextLine();
                                objectToEdit.setName(name2);
                                break;
                            case 2:
                                System.out.println("rok urodzenia");
                                int birthYear2 = scanner.nextInt();
                                objectToEdit.setBirthyear(birthYear2) ;
                                break;
                            case 3:
                                System.out.println("specjalizacja");
                                scanner.nextLine();
                                String specialization2 = scanner.nextLine();
                                objectToEdit.setSpecialization(specialization2); ;
                                break;
                            case 4:
                                System.out.println("pensja");
                                float salary1 = scanner.nextFloat();
                                objectToEdit.setSalary(salary1);
                                break;
                            case 5:
                                endEdit = 1;
                                break;
                            default:
                                System.out.println("zły numer");
                                break;
                        }

                    }
                     map.replace(idToEdit,objectToEdit);

                    break;
                case 5:

                    System.out.println("wpisz id do usuniecia");
                    long id = scanner.nextLong();
                    map.remove(id);
                    break;
                case 6:
                    quit = 1;

                    System.out.println("Wyjscie");
                    return;
                default:
                    System.out.println("Wybrano inny");
            }
        }
        return;
    }

    public static void EditAnimal(HazelcastInstance instance)  {
        int quit = 0;
        while(quit == 0){

            System.out.println("Choose from these choices");
            System.out.println("-------------------------\n");
            System.out.println("1 - Wyswietl wszystko");
            System.out.println("2 - Wyswietl gatunek lub rase");
            System.out.println("3 - Dodaj");
            System.out.println("4 - edytuj");
            System.out.println("5 - usun");
            System.out.println("6 - Quit");

            Scanner scanner = new Scanner(System.in);
            int choice = scanner.nextInt();
            IMap<Long, Animal> map = instance.getMap( "animals" );
            switch (choice) {
                case 1:

                    System.out.println("All animals: ");
                    for(Map.Entry<Long, Animal> e : map.entrySet()){
                        System.out.println(e.getKey() + " => " + e.getValue());
                    }
                    break;
                case 2:

                    int endsee = 0;
                    while (endsee == 0)
                    {

                        System.out.println("co chcesz wyświetlic");
                        System.out.println("-------------------------");
                        System.out.println("1 - rase");
                        System.out.println("2 - gatunek");
                        System.out.println("3 - koniec");
                        int choice2 = scanner.nextInt();

                        switch (choice2)
                        {
                            case 1:

                                System.out.println("podaj rase");
                                scanner.nextLine();
                                String name22 = scanner.nextLine();
                                for(Map.Entry<Long, Animal> e : map.entrySet()){
                                    if( e.getValue().breed.equals(name22))
                                    {

                                        System.out.println(e.getKey() + " => " + e.getValue());
                                    }
                                }
                            break;
                            case 2:
                                System.out.println("podaj gatunek");
                                scanner.nextLine();
                                String name23 = scanner.nextLine();
                                for(Map.Entry<Long, Animal> e : map.entrySet()){
                                    if( e.getValue().species.equals(name23))
                                    {

                                        System.out.println(e.getKey() + " => " + e.getValue());
                                    }
                                }
                            break;
                            case 3:
                                endsee = 1;
                                break;
                            default:
                                System.out.println("zły numer");
                                break;
                        }

                    }

                    break;
                case 3:
                    Long key1 = (long) Math.abs(r.nextInt());
                    System.out.println("podaj imie ");
                    scanner.nextLine();
                    String name = scanner.nextLine();
                    System.out.println("rok urodzenia");
                    int birthYear = scanner.nextInt();
                    System.out.println("uwagi");
                    scanner.nextLine();
                    String comments = scanner.nextLine();

                    System.out.println("wybieg");
                    int catwalk = scanner.nextInt();

                    System.out.println("wpisz gatunek");
                    scanner.nextLine();
                    String species = scanner.nextLine();

                    System.out.println("wpisz rase");
                    String breed = scanner.nextLine();

                    Animal animal1 = new Animal(name, birthYear,comments,catwalk, breed, species);

                    System.out.println("PUT " + key1 + " => " + animal1);
                    map.put(key1, animal1);
                    break;
                case 4:
                    System.out.println("wpisz id do edycji");
                    long idToEdit =  scanner.nextLong();;
                    Animal objectToEdit = map.get(idToEdit);
                    int endEdit = 0;
                    while (endEdit == 0)
                    {

                        System.out.println("co chcesz edytować");
                        System.out.println("-------------------------");
                        System.out.println("1 - imie");
                        System.out.println("2 - rok urodzenia");
                        System.out.println("3 - uwagi");
                        System.out.println("4 - wybieg");
                        System.out.println("5 - wpisz gatunek");
                        System.out.println("6 - wpisz rase");
                        System.out.println("7 - zakończ edycje");

                        int choice2 = scanner.nextInt();

                        switch (choice2)
                        {
                            case 1:

                                System.out.println("podaj nowe imie");
                                scanner.nextLine();
                                String name2 = scanner.nextLine();
                                objectToEdit.name =name2;
                                break;
                            case 2:
                                System.out.println("rok urodzenia");
                                int birthYear2 = scanner.nextInt();
                                objectToEdit.birthyear = birthYear2 ;
                                break;
                            case 3:
                                System.out.println("uwagi");
                                scanner.nextLine();
                                String uwagi2 = scanner.nextLine();
                                objectToEdit.comments = uwagi2; ;
                                break;
                            case 4:
                                System.out.println("wybieg");
                                int wybieg1 = scanner.nextInt();
                                objectToEdit.catwalk = wybieg1;
                                break;
                            case 5:
                                System.out.println("rasa");
                                scanner.nextLine();
                                String rasa2 = scanner.nextLine();
                                objectToEdit.breed = rasa2; ;
                                break;
                            case 6:
                                System.out.println("gatunek");
                                scanner.nextLine();
                                String gatunek2 = scanner.nextLine();
                                objectToEdit.species = gatunek2; ;
                                break;
                            case 7:
                                endEdit = 1;
                                break;
                            default:
                                System.out.println("zły numer");
                                break;
                        }

                    }
                    map.replace(idToEdit,objectToEdit);

                    break;
                case 5:

                    System.out.println("wpisz id do usuniecia");
                    long id = scanner.nextLong();
                    map.remove(id);
                    break;
                case 6:
                    quit = 1;

                    System.out.println("Wyjscie");
                    return;
                default:
                    System.out.println("Wybrano inny");
            }
        }
        return;
    }
}