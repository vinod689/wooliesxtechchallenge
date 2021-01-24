using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Application.CoreLogger
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                    
            }
        }

        private Task HandleExceptionAsync(HttpContext context,Exception ex)
        {

            WebHelper.LogWebError(product: "netCoreLogger.web", layer: "webAPI", ex, context);

            var errorId = Activity.Current?.Id ?? context.TraceIdentifier;

            var customError = $"ErrorID-{errorId}:Messag - some error occured in the API.";
            var result = JsonConvert.SerializeObject(customError);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }




    }
}
