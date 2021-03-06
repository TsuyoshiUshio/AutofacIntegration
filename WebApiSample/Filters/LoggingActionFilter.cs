﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace WebApiSample.Filters
{
    public class LoggingActionFilter : IAutofacActionFilter
    {
        readonly ILogger logger;

        public LoggingActionFilter(ILogger logger) {
            this.logger = logger;
        }


        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            logger.Write(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
			logger.Write(actionContext.ActionDescriptor.ActionName);
			return Task.FromResult(0);
        }
    }

    public interface ILogger
    {
        void Write(string message);
    }

    public class ConsoleLogger : ILogger
    {


public void Write(string message)
    {
        Console.WriteLine("[Logging]:" + message);
    }
    }
}
