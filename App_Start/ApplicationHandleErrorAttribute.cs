using Fiveways.Logging;
using System.Web.Http.ExceptionHandling;

namespace Fiveways.External.API.App_Start
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            ApplicationLog.Error(context.Exception.ToString());
            base.Handle(context);
        }
    }
}