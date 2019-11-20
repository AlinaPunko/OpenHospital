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
            cmd.CommandText = "Select * from SELECTALLVISITS";
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }

        public static DataView GetVisitsByPatientId(int patientId)
        {
            OracleCommand cmd = new OracleCommand("SelectVisitsByPatientID", App.con);
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

        public static void InsertVisit(Visit visits)
        {
            //TherapistContainer1 context = new TherapistContainer1();
            //if (visits.EntityState != System.Data.EntityState.Detached)
            //{
            //    context.ObjectStateManager.ChangeObjectState(visits, System.Data.EntityState.Added);
            //}
            //else
            //{
            //    context.Visits.AddObject(visits);
            //}
            //context.SaveChanges();
            //context.Detach(visits);
        }

        public static void UpdateVisit(Visit visits)
        {
            //OracleCommand cmd = new OracleCommand("Updatevisit", App.con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            //cmd.Parameters.Add("patientname", patient.Name);
            //cmd.Parameters.Add("patientaddress", patient.Address);
            //cmd.Parameters.Add("patientbirthdate", patient.Birthdate);
            //cmd.Parameters.Add("patientphone", patient.Phone);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            //int res = cmd.ExecuteNonQuery();
        }

        public static void DeleteVisitById(int dname, DateTime dtime)
        {
            OracleCommand cmd = new OracleCommand("DeleteVisit", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("dname", dname);
            cmd.Parameters.Add("dt", dtime);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
        }
    }
}
