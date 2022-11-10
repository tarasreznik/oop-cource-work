using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Hotel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();

        public override string ToString()
        {
            return "Hotel: " + Name + " Descriptions: " + Description;
        }
    }
}
