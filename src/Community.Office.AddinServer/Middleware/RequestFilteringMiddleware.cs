﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Community.Office.AddinServer.Middleware
{
    /// <summary>Represents add-in request filtering middleware.</summary>
    internal sealed class RequestFilteringMiddleware : IMiddleware
    {
        /// <summary>Initializes a new instance of the <see cref="RequestFilteringMiddleware" /> class.</summary>
        public RequestFilteringMiddleware()
        {
        }

        Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!StringSegment.Equals(context.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;

                return Task.CompletedTask;
            }

            return next.Invoke(context);
        }
    }
}