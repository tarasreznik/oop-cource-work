using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        UserService us = new UserService("");

        [TestMethod()]
        public void AddTest()
        {
            Assert.ThrowsException<HotelException>(() => us.Add("", "sa"), "Name can`t be null");
        }

        [TestMethod()]
        public void AddTest1()
        {
            Assert.ThrowsException<HotelException>(() => us.Add("sa", ""), "Surname can`t be null");
        }

        [TestMethod()]
        public void AddTest2()
        {
            us.Add("sa", "sa");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.ThrowsException<HotelException>(() => us.Remove(-1, new List<Hotel> { }), "Index out of range");
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            var a = new User { Name = "d" };
            us.Users.Add(a);
            us.Users.Add(new User { });
            us.Remove(0, new List<Hotel> { new Hotel { Rooms = new List<Room> { new Room { User = a, Rezerve = true } } } });
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.ThrowsException<HotelException>(() => us.Edit(-1, "sa", "sa", new List<Hotel>()), "Index out of range");
        }

        [TestMethod()]
        public void EditTest1()
        {
            us.Users.Add(new User { });
            Assert.ThrowsException<HotelException>(() => us.Edit(0, "", "sa", new List<Hotel>()), "Name can`t be null");
        }

        [TestMethod()]
        public void EditTest2()
        {
            us.Users.Add(new User { });
            Assert.ThrowsException<HotelException>(() => us.Edit(0, "фів", "", new List<Hotel>()), "Surname can`t be null");
        }

        [TestMethod()]
        public void EditTest3()
        {
            var a = new User { Name = "d" };
            us.Users.Add(a);
            us.Users.Add(new User { });
            us.Edit(0, "das", "sa", new List<Hotel> { new Hotel { Rooms = new List<Room> { new Room { User = a, Rezerve = true } } } });
        }

        [TestMethod()]
        public void Edit1Test()
        {
            Assert.ThrowsException<HotelException>(() => us.Edit(-1, 0), "Index out of range");
        }

        [TestMethod()]
        public void Edit1Test1()
        {
            us.Users.Add(new User { });
            Assert.ThrowsException<HotelException>(() => us.Edit(0, -1), "Day can`t be less then 0");
        }

        [TestMethod()]
        public void Edit1Test2()
        {
            us.Users.Add(new User { Day = 15});
            Assert.ThrowsException<HotelException>(() => us.Edit(0, 50), "Surname can`t be null");
        }

        [TestMethod()]
        public void Edit1Test3()
        {
            var a = new User { Name = "d" };
            us.Users.Add(a);
            us.Users.Add(new User { });
            us.Edit(0, 15);
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.ThrowsException<HotelException>(() => us.GetUser(-1), "Index out of range");
        }
    }
}