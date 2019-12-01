using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Visit
    {
        int id;
        Doctor doctor;
        Patient patient;
        DateTime dateTime;
        VisitType type;
        string symthoms;
        string prescription;
        string notes;
        Room room;
        string diagnosis;
        byte[] _file;


        public byte[] file { get => _file; set => _file = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public Patient Patient { get => patient; set => patient = value; }
      
        public VisitType Type { get => type; set => type = value; }
        public string Symthoms { get => symthoms; set => symthoms = value; }
        public string Prescription { get => prescription; set => prescription = value; }
        public string Notes { get => notes; set => notes = value; }
        public Room Room { get => room; set => room = value; }
        public string Diagnosis { get => diagnosis; set => diagnosis = value; }
        public int ID { get => id; set => id = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }

        public Visit()
        {
        }

        public Visit( int iD, Doctor doctor, Patient patient, DateTime dateTime, VisitType type, string symthoms,
            string diagnosis, string prescription, string notes, Room room, byte[] file)
        {
            ID = iD;
            Doctor = doctor;
            Patient = patient;
            DateTime = dateTime;
            Type = type;
            Symthoms = symthoms;
            Diagnosis = diagnosis;
            Prescription = prescription;
            Notes = notes;
            Room = room;
            this.file = file;
        }
    }
}
