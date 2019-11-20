using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Patient
    {
        int id;
        string name;
        DateTime birthdate;
        string address;
        string phone;

        public Patient()
        {}

        public Patient(int id, string name, DateTime birthdate, string address, string phone)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            Address = address;
            Phone = phone;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
    }
}
