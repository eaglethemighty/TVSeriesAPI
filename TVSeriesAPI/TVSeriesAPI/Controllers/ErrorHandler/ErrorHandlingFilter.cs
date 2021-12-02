using Microsoft.AspNetCore.Mvc.Filters;

namespace TVSeriesAPI.Controllers.ErrorHandler
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            ILogger logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger("ErrorHandlingFilter");
            logger.LogError(
                $"Exception message: {exception.Message} \n" +
                $"Source: {exception.Source}\n" +
                $"Target: {exception.TargetSite}");

            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true; //optional 
        }
    }
}
