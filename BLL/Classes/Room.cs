using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Room
    {
        public string Name { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
        public bool Rezerve { get; set; } = false;
        public double Price { get; set; }
        public int Place { get; set; }
    }
}
