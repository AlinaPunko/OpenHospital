using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class VisitType
    {
        int id;
        string type;

        public VisitType(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
    }
}
