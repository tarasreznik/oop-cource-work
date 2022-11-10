using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Day { get; set; } = 0;

        public override string ToString() => "Visitor: " + Name + " " + Surname + " Reserve: " + Day + " day(s)";
        
    }
}
