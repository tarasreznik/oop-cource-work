using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserService
    {
        public List<User> Users { get; private set; } = new List<User>();
        Serialize<User> serialize;
        public static UserService instanse;

        public UserService(string name)
        {
            instanse = this;
        }

        public UserService()
        {
            instanse = this;
            serialize = new Serialize<User>("Users");
            try { Users = serialize.Load().ToList(); }
            catch { serialize.Save(Users.ToArray());  }
        }

        public void Add(string name, string surname)
        {
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new HotelException("Name can`t be null");
            if (surname == null || String.IsNullOrEmpty(surname.Trim()))
                throw new HotelException("Surname can`t be null");
            Users.Add(new User { Name = name, Surname = surname });
        }

        public void Remove(int ind, List<Hotel> hotels)
        {
            if (ind < 0 || ind >= Users.Count)
                throw new HotelException("Index out of range");
            foreach (var hotel in hotels)
                foreach (var room in hotel.Rooms)
                    if (room.User == Users[ind])
                    {
                        room.User = null;
                        room.Rezerve = false;
                    }
            Users.RemoveAt(ind);
        }

        public void Edit(int ind, string name, string surname, List<Hotel> hotels)
        {
            if (ind < 0 || ind >= Users.Count)
                throw new HotelException("Index out of range");
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new HotelException("Name can`t be null");
            if (surname == null || String.IsNullOrEmpty(surname.Trim()))
                throw new HotelException("Surname can`t be null");
            foreach (var hotel in hotels)
                foreach (var room in hotel.Rooms)
                    if (room.User == Users[ind])
                    {
                        room.User.Name = name;
                        room.User.Surname = surname;
                    }
            Users[ind].Name = name;
            Users[ind].Surname = surname;
        }

        public void Edit(int ind, int day)
        {
            if (ind < 0 || ind >= Users.Count)
                throw new HotelException("Index out of range");
            if (day < 0)
                throw new HotelException("Day can`t be less then 0");
            if (Users[ind].Day != 0)
                throw new HotelException("This user have alredy reserve room");
            Users[ind].Day = day;
        }

        public List<User> SortByName()
        {
            var temp = Users;
            for(int i = 0; i < Users.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Users.Count; j++)
                    if (temp[min].Name.CompareTo(temp[j].Name) > 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;
            }
            return temp;
        }

        public List<User> SortBySurname()
        {
            var temp = Users;
            for (int i = 0; i < Users.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Users.Count; j++)
                    if (temp[min].Name.CompareTo(temp[j].Name) > 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;
            }
            return temp;
        }

        public List<User> Find(string key)
        {
            if (Regex.IsMatch(key, "[0-9]+"))
                return Users.FindAll(x => x.Day == int.Parse(key));
            var temp = Users.FindAll(x => x.Name == key);
            if (temp != null)
                return temp;
            else
            {
                temp = Users.FindAll(x => x.Surname == key);
                if (temp != null)
                    return temp;
                else throw new HotelException("You have no users that containt key word: " + key);
            }
        }

        public User GetUser(int ind)
        {
            if (ind < 0 || ind >= Users.Count)
                throw new HotelException("Index out of range");
            return Users[ind];
        }

        public void Save() => serialize.Save(Users.ToArray());
    }
}
