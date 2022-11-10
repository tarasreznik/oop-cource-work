using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;

namespace Hotel
{
    public class Menu
    {
        HotelService hs = new HotelService();
        ReserveService rs = new ReserveService();
        UserService us = new UserService();

        private int pars(string number) => !Regex.IsMatch(number, "[0-9]") ? -1 : int.Parse(number) - 1;

        private int UserInd()
        {
            int count = 1;
            foreach (var a in us.Users)
                Console.WriteLine((count++) + ". Client: " + a.Name + " " + a.Surname);
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        private int HotelInd()
        {
            int count = 1;
            foreach (var a in hs.Hotels)
                Console.WriteLine((count++) + ". Hotel: " + a.Name + " Description: " + a.Description);
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        private int RoomInd(BLL.Hotel hotel)
        {
            int count = 1;
            foreach (var a in hotel.Rooms)
                Console.WriteLine((count++) + ". Room: " + a.Name + " Place in room: " + a.Place + " Price for day: " + a.Price);
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        public void Run()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1. Users\n2. Hotels\n3. Reserve\n4. Find\n0. Exit");
                Console.Write("Action: ");
                switch(Console.ReadLine())
                {
                    case "0":
                        hs.Save();
                        us.Save();
                        return;
                    case "1":
                        Users();
                        break;
                    case "2":
                        Hotels();
                        break;
                    case "3":
                        Reserve();
                        break;
                    case "4":
                        Find();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Users()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Users\n1. Add\n2. Remove\n3. Edit\n4. Show all\n5. Sort by name\n6. Sort by surname\n0. Back");
                Console.Write("Action: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Surname: ");
                            us.Add(name, Console.ReadLine());
                        }
                        catch(Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select client:");
                            us.Remove(UserInd(), hs.Hotels);
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Select client:");
                            int ind = UserInd();
                            Console.Write("New name: ");
                            var name = Console.ReadLine();
                            Console.Write("New surname: ");
                            us.Edit(ind, name, Console.ReadLine(), hs.Hotels);
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        foreach (var a in us.Users)
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    case "5":
                        foreach (var a in us.SortByName())
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    case "6":
                        foreach (var a in us.SortBySurname())
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();                            
                        break;
                }
            }
        }

        private void Hotels()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hotels\n1. Add\n2. Remove\n3. Show all\n4. Add room\n5. Remove roon\n6. Show concrate hotel\n0. Back");
                Console.Write("Action: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Description: ");
                            hs.AddHotel(name, Console.ReadLine());
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select hotel:");
                            hs.RemoveHotel(HotelInd());
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        foreach (var a in hs.Hotels)
                            Console.WriteLine("Hotel: " + a.Name + " Description: " + a.Description);
                        Console.ReadKey();
                        break;
                        case "4":
                        try
                        {
                            Console.WriteLine("Select hotel:");
                            int ind = HotelInd();
                            Console.Write("Room number: ");
                            var name = Console.ReadLine();
                            Console.Write("Number of place: ");
                            var nop = Console.ReadLine();
                            Console.Write("Price for day: ");
                            hs.AddRoom(ind, name, pars(nop) + 1, pars(Console.ReadLine()));

                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("Select hotel:");
                            int ind = HotelInd();
                            Console.WriteLine("Select room:");
                            hs.RemoveRoom(ind, RoomInd(hs.GetHotel(ind)));

                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                  
                    case "6":
                        try
                        {
                            Console.WriteLine("Select hotel:");
                            Console.WriteLine(hs.ShowHotel(HotelInd())) ;
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Reserve()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Users\n1. Add\n2. Remove\n3. Show concrate\n4. Show all reserve\n5. Show all unreserved\n0. Back");
                Console.Write("Action: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.WriteLine("Select hotel");
                            int hind = HotelInd();
                            Console.WriteLine("Select room");
                            int rInd = RoomInd(hs.GetHotel(hind));
                            Console.WriteLine("Select client");
                            int ind = UserInd();
                            Console.Write("Days: ");
                            rs.AddReserve(hs.GetRoom(hind, rInd), ind, int.Parse(Console.ReadLine()));
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select hotel");
                            int hind = HotelInd();
                            Console.WriteLine("Select room");
                            int rInd = RoomInd(hs.GetHotel(hind));
                            rs.RemoveReserve(hs.GetRoom(hind, rInd));
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Select hotel");
                            int hind = HotelInd();
                            Console.WriteLine("Select room");
                            int rInd = RoomInd(hs.GetHotel(hind));
                            var a = hs.GetRoom(hind, rInd);
                            string str = a.Rezerve ? "Room: " + a.Name + " Reservetor: " + a.User.Name + " " +a.User.Surname + " Total cost: " + a.User.Day * a.Price : "Room: " + a.Name + " Reserve: " + a.Rezerve;
                            Console.WriteLine(str); Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine("Select hotel");
                            int hind = HotelInd();
                            Console.WriteLine("All reserved place");
                            foreach (var a in hs.GetHotel(hind).Rooms)
                                if (a.Rezerve)
                                    Console.WriteLine("Room: " + a.Name + " Reservetor: " + a.User.Name + " " + a.User.Surname);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("Select hotel");
                            int hind = HotelInd();
                            Console.WriteLine("All unreserved place");
                            foreach (var a in hs.GetHotel(hind).Rooms)
                                if (!a.Rezerve)
                                    Console.WriteLine("Room: " + a.Name);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Find()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Clients\n2. Hotels\n0. Exit");
                Console.Write("Action: ");
                switch (Console.ReadLine())
                {
                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Key word: ");
                            foreach (var a in us.Find(Console.ReadLine()))
                                Console.WriteLine(a);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.Write("Key word: ");
                            foreach (var a in hs.Find(Console.ReadLine()))
                                Console.WriteLine(a);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
