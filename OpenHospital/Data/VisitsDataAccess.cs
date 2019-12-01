using OpenHospital.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Data
{
    class VisitsDataAccess
    {
        public static DataView GetVisits()
        {
            OracleCommand cmd = new OracleCommand("GetAllVisits", App.con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from admin.SELECTALLVISITS";
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }

        public static DataView GetVisitsByPatientId(int patientId)
        {
            OracleCommand cmd = new OracleCommand("Admin.SelectVisitsByPatientID", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pID", patientId);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            OracleDataReader reader  = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.AsDataView();
        }

        public static DataView GetVisitsByDoctorId(int doctorId)
        {
            OracleCommand cmd = new OracleCommand("admin.selectvisitsbydoctorid", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("doctorid", doctorId);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt.AsDataView();
        }

        ////public static Visit GetVisitsById(int visitsId)
        ////{
        ////    TherapistContainer1 context = new TherapistContainer1();
        ////    var visits = context
        ////                        .Visits
        ////                        .Include("Doctor")
        ////                        .Include("Patient")
        ////                        .Where(p => p.VisitID == visitsId)
        ////                        .FirstOrDefault();
        ////    //var visits = context.Visits.Where(p => p.VisitID == visitsId).FirstOrDefault();
        ////    context.Detach(visits);
        ////    return visits;
        ////}

        public static void InsertVisit(Visit visit)
        {
            OracleCommand cmd = new OracleCommand("admin.Addvisit", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("visitdoctor", visit.Doctor.Id);
            cmd.Parameters.Add("visitpatient", visit.Patient.Id);
            cmd.Parameters.Add("visitdatetime", visit.DateTime);
            cmd.Parameters.Add("visittype", visit.Type.Id);
            cmd.Parameters.Add("visitsympthoms", visit.Symthoms);
            cmd.Parameters.Add("visitdiagnosis", visit.Diagnosis);
            cmd.Parameters.Add("visitprescription", visit.Prescription);
            cmd.Parameters.Add("visitnotes", visit.Notes);
            cmd.Parameters.Add("visitroom", visit.Room.Number);
            cmd.Parameters.Add("visitfile", OracleDbType.Blob, visit.file, ParameterDirection.Input);
            int res = cmd.ExecuteNonQuery();
        }

        public static void UpdateVisit(Visit visit)
        {
            OracleCommand cmd = new OracleCommand("admin.Updatevisit", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("visitid", visit.ID);
            cmd.Parameters.Add("visitdoctor", visit.Doctor.Id);
            cmd.Parameters.Add("visitpatient", visit.Patient.Id);
            cmd.Parameters.Add("visitdatetime", visit.DateTime);
            cmd.Parameters.Add("visittype", visit.Type.Id);
            cmd.Parameters.Add("visitsympthoms", visit.Symthoms);
            cmd.Parameters.Add("visitdiagnosis", visit.Diagnosis);
            cmd.Parameters.Add("visitprescription", visit.Prescription);
            cmd.Parameters.Add("visitnotes", visit.Notes);
            cmd.Parameters.Add("visitroom", visit.Room.Number);
            cmd.Parameters.Add("visitfile", OracleDbType.Blob, visit.file, ParameterDirection.Input);
            int res = cmd.ExecuteNonQuery();
        }

        public static void DeleteVisitById(int did)
        {
            OracleCommand cmd = new OracleCommand("admin.DeleteVisit", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("did", did);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
        }
        public static Visit GetVisitByID(int visitid)
        {
            OracleCommand cmd = new OracleCommand("admin.GetVisitByID", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("did", visitid);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                Visit visit = new Visit();
                visit.DateTime = Convert.ToDateTime(dt[3].ToString());
                visit.Room = new Room(dt[9].ToString(), null);
                visit.ID = Convert.ToInt32(dt[0]);
                visit.Symthoms = dt[5].ToString();
                visit.Diagnosis = dt[6].ToString();
                visit.Prescription = dt[7].ToString();
                visit.Notes = dt[8].ToString();
                visit.Patient = PatientsDataAccess.GetPatientByName(dt[2].ToString());
                visit.Doctor = DoctorDataAccess.GetDoctorByName(dt[1].ToString());
                if (dt[10].ToString()!="")
                    visit.file = (byte[])dt[10];
                return visit;
            }
            else return null;

        }

        internal static VisitType GetTypeByName(string v)
        {
            OracleCommand cmd = new OracleCommand("admin.GetTypeByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("vtype", v);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                VisitType visitType = new VisitType(Convert.ToInt32(dt[0]), dt[1].ToString());
                return visitType;
            }
            else return null;
        }
    }
}
