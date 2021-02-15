using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Business.Helpers
{
    public static class ControllerHelper
    {
        public static ProblemDetails CreateProblemDetails(Exception exception, ILogger logger, string logMessageTemplate, params string[] logArgs)
        {
            if (logger != null)
            {
                logger.LogError(new EventId(400), exception, logMessageTemplate, logArgs);
            }

            return CreateProblemDetails(exception);
        }

        public static ProblemDetails CreateProblemDetails(Exception exception, ILogger logger, string methodName)
        {
            if (logger != null)
            {
                logger.LogError(new EventId(400), exception, "Error occurred while trying execute method {MethodName}", methodName);
            }

            return CreateProblemDetails(exception);
        }

        public static ProblemDetails CreateProblemDetails(int status, string title, string message, ILogger logger, string logMessageTemplate, params string[] logArgs)
        {
            if (logger != null)
            {
                logger.LogError(new EventId(status), logMessageTemplate, logArgs);
            }

            return CreateProblemDetails(status, title, message);
        }

        public static ProblemDetails CreateProblemDetails(Exception exception, int status, string title, string message, ILogger logger, string logMessageTemplate, params string[] logArgs)
        {
            if (logger != null)
            {
                logger.LogError(new EventId(status), exception, logMessageTemplate, logArgs);
            }

            return CreateProblemDetails(status, title, message);
        }

        public static ProblemDetails CreateProblemDetails(Exception exception)
        {
            return CreateProblemDetails(exception, 400);
        }

        public static ProblemDetails CreateProblemDetails(Exception exception, int status)
        {
            if (exception is AppException appex)
            {
                return CreateProblemDetails(status, appex.Title, appex.Message);
            }

            return CreateProblemDetails(status, "Bad request", "Error occurred (details hidden)");
        }

        public static ProblemDetails CreateProblemDetails(int status, string title, string message)
        {
            return new ProblemDetails { Status = status, Title = title, Detail = message };
        }
    }
}
