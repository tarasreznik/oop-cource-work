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
    public class ReserveServiceTests
    {

        private ReserveService rs = new ReserveService();

        [TestMethod()]
        public void AddReserveTest()
        {
            Assert.ThrowsException<HotelException>(() => rs.AddReserve(null, 0, 1), "Room cant be null");
        }

        [TestMethod()]
        public void AddReserveTest1()
        {
            Assert.ThrowsException<HotelException>(() => rs.AddReserve(new Room { Rezerve = true }, 0, 1), "This room alrady reserve");
        }

        [TestMethod()]
        public void AddReserveTest2()
        {
            UserService userService = new UserService("15");
            userService.Users.Add(new User { });
            rs.AddReserve(new Room { }, 0, 1);
        }

        [TestMethod()]
        public void RemoveReserveTest()
        {
            Assert.ThrowsException<HotelException>(() => rs.RemoveReserve(null), "Room cant be null");
        }
        [TestMethod()]
        public void RemoveReserveTest1()
        {
            Assert.ThrowsException<HotelException>(() => rs.RemoveReserve(new Room { }), "Room cant be null");
        }
        [TestMethod()]
        public void RemoveReserveTest2()
        {
            UserService userService = new UserService("15");
            User user = new User { Day = 15 };
            userService.Users.Add(user);
            rs.RemoveReserve(new Room { Rezerve = true , User = user });
        }
    }
}