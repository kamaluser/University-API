﻿using Service.Exceptions;

namespace University_API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                var message = ex.Message;
                var errors = new List<RestExceptionError>();
                context.Response.StatusCode = 500;

                if (ex is RestException rex)
                {
                    message = rex.Message;
                    errors = rex.Errors;
                    context.Response.StatusCode = rex.Code;
                }

                await context.Response.WriteAsJsonAsync(new { message, errors });
            }
        }
    }
}
