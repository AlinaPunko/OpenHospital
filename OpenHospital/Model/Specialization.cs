using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Specialization
    {
        int id;
        string specialization;

        public int Id { get => id; set => id = value; }
        public string Specialization1 { get => specialization; set => specialization = value; }

        public Specialization(int id, string specialization1)
        {
            Id = id;
            Specialization1 = specialization1;
        }
    }
}
