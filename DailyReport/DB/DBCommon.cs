using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReport.DB
{
    public class DBCommon
    {
        private string connectionString;
        public string ConnectionString 
        {
            get
            {
                return connectionString;
            }
        }
    }
}