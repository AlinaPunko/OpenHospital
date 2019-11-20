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
        public static int countdoctors()
        {
            OracleCommand cmd = new OracleCommand("CountDoctors", App.con);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter countd = new OracleParameter("countd", OracleDbType.Int32);
            cmd.Parameters.Add(countd).Direction = System.Data.ParameterDirection.Output;
            var dt = cmd.ExecuteReader();
            return countd;
        }
        public static int countpatients()
        {

        }
        public static int countspec()
        {

        }
        public static int countvisits()
        {

        }
        public static int visitslastmonth()
        {

        }
        public static int countrooms()
        {

        }
        public static int counthigh()
        {

        }
        public static int countfirst()
        {

        }
        public static int countsecond()
        {

        }

    }
}
