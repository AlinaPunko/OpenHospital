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
    public class DoctorDataAccess
    {
        public static DataView GetDoctorsBySpec(string spec)
        {
            OracleCommand cmd = new OracleCommand("admin.selectdoctorsbyspec", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("spec", spec);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }
        public static DataView GetDoctorsByCat(string cat)
        {
            OracleCommand cmd = new OracleCommand("admin.selectdoctorsbycat", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cat", cat);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }

        public static DataView FindDoctorByName(string dname)
        {
            OracleCommand cmd = new OracleCommand("admin.finddoctorsbyname", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("dname", "%"+dname+"%");
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }
        public static DataView GetDoctors()
        {
            OracleCommand cmd = new OracleCommand("GetAllDoctors", App.con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Admin.SELECTALLDOCTORS";
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            // return dt.AsEnumerable();
            //DataView dataView = dt.AsDataView(); 
            return dt.AsDataView();
        }

        public static Doctor GetDoctorById(int doctorId)
        {
            Doctor doctor = new Doctor();
            OracleCommand cmd = new OracleCommand("admin.GetDoctorByID", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("did", doctorId);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            while (dt.Read())
            {
                doctor.Id = Convert.ToInt32(dt["ID"]);
                doctor.Name = dt["Name"].ToString();
                doctor.Address = dt["Address"].ToString();
                doctor.Phone = dt["Phone"].ToString();
                //doctor.Category1.Category1 = dt["Category"].ToString();
                //doctor.Specialization1.Specialization1 = dt["Spec"].ToString();
                dt.NextResult();
            }
            return doctor;
        }

        public static void InsertDoctor(Doctor doctor)
        {
            OracleCommand cmd = new OracleCommand("admin.Adddoctor", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("doctorname", doctor.Name);
            cmd.Parameters.Add("doctoraddress", doctor.Address);
            cmd.Parameters.Add("doctorphone", doctor.Phone);
            cmd.Parameters.Add("doctorcat", doctor.Category1.Id);
            cmd.Parameters.Add("doctorspec", doctor.Specialization1.Id);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
        }

        public static int SelectSpecIdByName(string text)
        {
            //Doctor doctor = new Doctor();
            OracleCommand cmd = new OracleCommand("Admin.SelectSpecIdByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("Sname", text);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                return Convert.ToInt32(dt["Id"].ToString());
            }
            else return 0;// doctor;
        }

        public static string SelectCatIdByCat(string text)
        {
            OracleCommand cmd = new OracleCommand("Admin.SelectCatIdByCat", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("Cname", text);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                return dt["Id"].ToString();
            }
            else return null;// doctor;
        }

        public static void UpdateDoctor(Doctor doctor)
        {
            OracleCommand cmd = new OracleCommand("admin.Updatedoctor", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("doctorid", doctor.Id);
            cmd.Parameters.Add("doctorname", doctor.Name);
            cmd.Parameters.Add("doctoraddress", doctor.Address);
            cmd.Parameters.Add("doctorphone", doctor.Phone);
            cmd.Parameters.Add("doctorcat", doctor.Category1.Id);
            cmd.Parameters.Add("doctorspec", doctor.Specialization1.Id);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            int res = cmd.ExecuteNonQuery();
        }
        public static Doctor SelectDoctorByName(string dname)
        {
            Doctor doctor = new Doctor();
            OracleCommand cmd = new OracleCommand("admin.SelectDoctorsByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("dname", dname);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            while (dt.Read())
            {
                doctor.Id = Convert.ToInt32(dt["ID"]);
                doctor.Name = dt["Name"].ToString();
                doctor.Address = dt["Address"].ToString();
                doctor.Phone = dt["Phone"].ToString();
                //doctor.Category1.Category1 = dt["Category"].ToString();
                //doctor.Specialization1.Specialization1 = dt["Spec"].ToString();
                dt.NextResult();
            }
            return doctor;
        }

        public static void DeleteDoctorById(int doctorId)
        {
            OracleCommand cmd = new OracleCommand("admin.DeleteDoctor", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            //OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add("doctorID", doctorId);
            //cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
        }


        public static Doctor GetDoctorByName(string name)
        {
            Doctor doctor = new Doctor();
            OracleCommand cmd = new OracleCommand("admin.GetDoctorsByName", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("dname", name);
            OracleParameter user_par = new OracleParameter("prc", OracleDbType.RefCursor);
            cmd.Parameters.Add(user_par).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            if (dt.Read())
            {
                doctor.Id = Convert.ToInt32(dt[0].ToString());
                doctor.Name = dt[1].ToString();
                doctor.Address = dt[2].ToString();
                doctor.Phone = dt[3].ToString();
                doctor.Category1 = new Category(SelectCatIdByCat(dt[4].ToString()), dt[4].ToString());
                doctor.Specialization1 = new Specialization(SelectSpecIdByName(dt[5].ToString()), dt[5].ToString());
            }
            return doctor;
        }

    }
}
