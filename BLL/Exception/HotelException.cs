using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HotelException : Exception
    {
        private string message;
        public HotelException() : base(){ }

        public HotelException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
