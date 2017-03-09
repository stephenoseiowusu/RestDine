using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDine.App_Start
{
    public static class NLogConfig
    {
        public static Logger nLogger()
        {
            return LogManager.GetCurrentClassLogger() ;
        }
    }
}