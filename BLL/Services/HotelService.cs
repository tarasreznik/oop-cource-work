using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class HotelService
    {
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        private Serialize<Hotel> serialize;

        public HotelService(string name) { }

        public HotelService()
        {
            serialize = new Serialize<Hotel>("Hotel");
            try { Hotels = serialize.Load().ToList(); }
            catch { serialize.Save(Hotels.ToArray()); }
        }

        public void AddHotel(string name, string description)
        {
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new HotelException("Name can`t be null");
            if (description == null || String.IsNullOrEmpty(description.Trim()))
                throw new HotelException("Description can`t be null");
            Hotels.Add(new Hotel { Name = name, Description = description });
        }

        public void RemoveHotel(int ind)
        {
            if (ind < 0 || ind >= Hotels.Count)
                throw new HotelException("Index out of range");
            foreach (var room in Hotels[ind].Rooms)
                foreach (var user in UserService.instanse.Users)
                    if (user == room.User)
                        user.Day = 0;
            Hotels.RemoveAt(ind);
        }

        public void EditHotel(int ind, string name, string description)
        {

            if (ind < 0 || ind >= Hotels.Count)
                throw new HotelException("Index out of range");
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new HotelException("Name can`t be null");
            if (description == null || String.IsNullOrEmpty(description.Trim()))
                throw new HotelException("Description can`t be null");
            Hotels[ind].Name = name;
            Hotels[ind].Description = description;
        }

        public void AddRoom(int ind, string number, int place, int price)
        {
            if (ind < 0 || ind >= Hotels.Count)
                throw new HotelException("Index out of range");
            if (!Regex.IsMatch(number, "[0-9]{3}"))
                throw new HotelException("Wrong room number");
            if (place < 0 || place > 4)
                throw new HotelException("Invalid number of place");
            if(price < 0)
                throw new HotelException("Price can`t be less than 0");
            Hotels[ind].Rooms.Add(new Room { Name = number, Place = place, Price = price, Hotel = Hotels[ind] });
        }

        public void RemoveRoom(int hotelInd, int roomInd)
        {
            if(hotelInd < 0 || hotelInd >= Hotels.Count)
                throw new HotelException("Index out of range");
            if(roomInd < 0 || roomInd >= Hotels[hotelInd].Rooms.Count)
                throw new HotelException("Index out of range");
            foreach (var user in UserService.instanse.Users)
                if (user == Hotels[hotelInd].Rooms[roomInd].User)
                    user.Day = 0;
            Hotels[hotelInd].Rooms.RemoveAt(roomInd);
        }

        public Hotel GetHotel(int ind) => ind < 0 || ind >= Hotels.Count ? throw new HotelException("Index out of range") : Hotels[ind];

        public Room GetRoom(int ind, int rInd)
        {
            if (ind < 0 || ind >= Hotels.Count)
                throw new HotelException("Index out of range");
            if (rInd < 0 || rInd >= Hotels[ind].Rooms.Count)
                throw new HotelException("Index out of range");
            return Hotels[ind].Rooms[rInd];
        }

        public List<Hotel> Find(string key)
        {
            var temp = Hotels.FindAll(x => x.Name == key);
            if (temp != null)
                return temp;
            else
            {
                temp = Hotels.FindAll(x => x.Description == key);
                if (temp != null)
                    return temp;
                else throw new HotelException("You have no users that containt key word: " + key);
            }
        }

        public string ShowHotel(int ind)
        {
            if (ind < 0 || ind >= Hotels.Count)
                throw new HotelException("Index out of range");
            string str = "";
            int all = 0;
            str +="Hotel: "+ Hotels[ind].Name + "\n";
            foreach(var a in Hotels[ind].Rooms)
            {
                all += a.Place;
                str += "Room: " + a.Name + " Reserve: " + a.Rezerve + "\n";
            }
            str += "All place: " + all;
            return str;
        }

        public void Save() => serialize.Save(Hotels.ToArray());
    }
}
