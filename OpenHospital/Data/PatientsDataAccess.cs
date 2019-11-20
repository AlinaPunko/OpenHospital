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
    class PatientsDataAccess
    {
        public static DataView GetPatients()
        {
            OracleCommand cmd = new OracleCommand("GetAllPatients", App.con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from admin.SELECTALLPATIENTS";
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }

        public static Patient GetPatientById(int patientId)
        {
            Patient patient = new Patient();
            OracleCommand cmd = new OracleCommand("GetPatientByID", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pID", patientId);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            while (dt.Read())
            {
                patient.Id = Convert.ToInt32(dt["ID"]);
                patient.Name = dt["Name"].ToString();
                patient.Address = dt["Address"].ToString();
                patient.Phone = dt["Phone"].ToString();
                patient.Birthdate =Convert.ToDateTime(dt["Birthdate"].ToString());
                dt.NextResult();
            }
            return patient;
        }

        //public static Patient GetPatientWithVisitsById(int patientId)
        //{
        //    //TherapistContainer1 context = new TherapistContainer1();
        //    //var patient = context.Patients
        //    //                    .Include("Consultations")
        //    //                    .Include("Visit")
        //    //                    .Where(p => p.PatientID == patientId)
        //    //                    .FirstOrDefault();
        //    //context.Detach(patient);
        //    //return patient;
        //}

        public static Patient InsertPatient(Patient patient)
        {
            OracleCommand cmd = new OracleCommand("Admin.Addpatient", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("patientname", patient.Name);
            cmd.Parameters.Add("patientbirthdate", OracleDbType.Date).Value = patient.Birthdate;
            cmd.Parameters.Add("patientaddress", patient.Address);

            //cmd.Parameters.Add("patientbirthdate", patient.Birthdate.ToString());
            cmd.Parameters.Add("patientphone", patient.Phone);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
            OracleCommand cmd1 = new OracleCommand("Admin.Getpatientbyname", App.con);
            cmd1.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd1.Parameters.Add("pname", patient.Name);
            cmd1.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd1.ExecuteReader();
            if (dt.Read())
            {
                patient.Id = Convert.ToInt32(dt["ID"]);
                patient.Name = dt["Name"].ToString();
                patient.Address = dt["Address"].ToString();
                patient.Phone = dt["Phone"].ToString();
                patient.Birthdate = Convert.ToDateTime(dt["Birthdate"].ToString());
                    return patient;
            }
            else return null;

        }

        public static void UpdatePatient(Patient patient)
        {
            OracleCommand cmd = new OracleCommand("Updatepatient", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //racleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("patientid", patient.Id);
            cmd.Parameters.Add("patientname", patient.Name);
            cmd.Parameters.Add("patientbirthdate", OracleDbType.Date).Value=patient.Birthdate;
            cmd.Parameters.Add("patientaddress", patient.Address);
            cmd.Parameters.Add("patientphone", patient.Phone);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
        }


        public static void DeletePatientById(int patientId)
        {
            OracleCommand cmd = new OracleCommand("DeletePatient", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("pid", patientId);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
        }

        public static int GetPatientByName(string name)
        {
            Patient patient = new Patient();
            OracleCommand cmd = new OracleCommand("GetPatientByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pname", name);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                return Convert.ToInt32(dt["id"].ToString());
            }
            else return 0;
        }
    }
}
