using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Room
    {
        string number;
        RoomType type;

        public Room(string number, RoomType type)
        {
            Number = number;
            Type = type;
        }

        public string Number { get => number; set => number = value; }
        internal RoomType Type { get => type; set => type = value; }
    }
}
