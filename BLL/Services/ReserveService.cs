using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReserveService
    {
        public void AddReserve(Room room, int userInd, int day)
        {
            if (room == null)
                throw new HotelException("Room cant be null");
            if (room.Rezerve)
                throw new HotelException("This room alrady reserve");
            UserService.instanse.Edit(userInd, day);
            room.User = UserService.instanse.GetUser(userInd);
            room.Rezerve = true;
        }

        public void RemoveReserve(Room room)
        {
            if (room == null)
                throw new HotelException("Room cant be null");
            if(!room.Rezerve)
                throw new HotelException("This room not reserve");
            UserService.instanse.Users.Find(x => x == room.User).Day = 0;
            room.User = null;
            room.Rezerve = false;
        }
    }
}
