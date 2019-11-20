using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class RoomType
    {
        string id;
        string type;

        public RoomType(string id, string type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
    }
}
