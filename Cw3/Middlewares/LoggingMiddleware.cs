using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cw3.Middlewares {
    public class LoggingMiddleware {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            context.Request.EnableBuffering();
            if (context.Request != null) {
                var method = context.Request.Method; //GET,POST
                string path = context.Request.Path; // api/students
                var queryString = context.Request.QueryString.ToString();
                string bodyStr;

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true)) {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                string[] lines = {"Request at: " + DateTime.Now,
                    "Method: " + method,
                    "Path: " + path,
                    "Query: " + queryString,
                    "Body: " + bodyStr + "\n"};
                File.AppendAllLines("requestsLog.txt", lines);
            }
            if (_next != null) await _next(context);
        }
    }
}