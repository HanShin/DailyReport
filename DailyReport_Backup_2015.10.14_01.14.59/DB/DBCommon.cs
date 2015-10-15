using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
namespace DailyReport.DB
{
    public static class DBCommon
    {
        private static string connectionString = @"Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST =172.22.118.71)(PORT = 1521)) ) (CONNECT_DATA = (SERVICE_NAME = SEP.DAELIM.COM))); User Id=system; Password=ORACLE;";
        
    }
}