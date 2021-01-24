using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesXTechChallenge.Application.CoreLogger
{
    public static class WebHelper
    {
        public static void LogWebError(string product, string layer,Exception ex,HttpContext context)
        {
            var details = new LogDetailModel();
            details.Exception = ex;

            AppLogger.WriteError(details);
        }
    }
}
