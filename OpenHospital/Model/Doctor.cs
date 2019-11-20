using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Doctor
    {
        int id;
        string name;
        string address;
        string phone;
        Category category;
        Specialization specialization;

        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        internal Specialization Specialization1 { get => specialization; set => specialization = value; }
        internal Category Category1 { get => category; set => category = value; }

        public Doctor(string phone, string address, string name, int id, Specialization specialization1, Category category1)
        {
            Phone = phone;
            Address = address;
            Name = name;
            Id = id;
            Specialization1 = specialization1;
            Category1 = category1;
        }

        public Doctor()
        {
        }
    }
}
