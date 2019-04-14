using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Attendance_Tracker
{
    class Helper
    {
        public static String CnnVal(String name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
