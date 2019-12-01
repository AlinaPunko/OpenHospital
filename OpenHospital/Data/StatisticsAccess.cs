using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Data
{
    class StatisticsAccess
    {
        public static string countdoctors()
        {
            OracleCommand cmd = new OracleCommand("admin.CountDoctors", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("countd", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return cmd.Parameters["countd"].Value.ToString();
        }
        public static string countpatients()
        {
            OracleCommand cmd = new OracleCommand("admin.CountPatients", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return cmd.Parameters["counth"].Value.ToString();
        }
        public static string countspec()
        {
            OracleCommand cmd = new OracleCommand("admin.CountSpec", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counts", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return cmd.Parameters["counts"].Value.ToString();
        }
        public static string countvisits()
        {
            OracleCommand cmd = new OracleCommand("admin.CountVisits", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return cmd.Parameters["counth"].Value.ToString();
        }
        public static string visitslastmonth()
        {
            OracleCommand cmd = new OracleCommand("Countvisitsbymonth", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string countrooms()
        {
            OracleCommand cmd = new OracleCommand("CountRooms", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string counthigh()
        {
            OracleCommand cmd = new OracleCommand("Counthigh", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string countfirst()
        {
            OracleCommand cmd = new OracleCommand("CountFirst", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string countsecond()
        {
            OracleCommand cmd = new OracleCommand("CountSecond", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string countchildren()
        {
            OracleCommand cmd = new OracleCommand("Countinfants", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
        public static string countretiree()
        {
            OracleCommand cmd = new OracleCommand("Countretiree", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("counth", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteNonQuery();
            return countd.Value.ToString();
        }
    }
}
