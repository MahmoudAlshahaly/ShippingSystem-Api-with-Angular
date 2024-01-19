using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Shipping.API.middelware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string requestBody = "";
            try
            {

                context.Response.ContentType = "application/json";
                //var body = context.Request.BodyReader.AsStream(true);
                //using (var streamReader = new StreamReader(body))
                //{
                    
                //    json = streamReader.ReadToEnd().ToString();
                //}
                //Read body from the request and log it
                using (var reader = new StreamReader(context.Request.Body))
                {
                    requestBody =await reader.ReadToEndAsync();
                    context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
                    //As this is a middleware below line will //make sure it will log each and every request body
                    //_logger.LogInformation(requestBody);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, requestBody);
            }
        }

        private  static Task HandleExceptionAsync(HttpContext context, Exception ex,string body)
        {
            
            
            var response = new
            {
                error = new
                {
                    message = "An error occurred while processing your request.",
                    details = ex.Message,
                    CODE= context.Response.StatusCode,
                    Body= body.Replace(" ",""),
                    METHODNAME = new StackTrace(ex).GetFrame(0).GetMethod().Name,

                }
            };


           return  context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
