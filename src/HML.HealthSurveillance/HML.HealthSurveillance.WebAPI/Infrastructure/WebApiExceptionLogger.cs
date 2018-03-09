using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using HML.HealthSurveillance.Common.Interfaces;

namespace HML.HealthSurveillance.WebAPI.Infrastructure
{
	public class WebApiExceptionLogger : ExceptionLogger
	{
		private readonly ILogger _logger;

		public WebApiExceptionLogger(ILogger logger)
		{
			_logger = logger;
		}

		public override void Log(ExceptionLoggerContext context)
		{
			_logger.LogError("WebApiExceptionLogger",context.ExceptionContext.Exception);
			base.Log(context);
		}

		public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			_logger.LogError("WebApiExceptionLogger", context.ExceptionContext.Exception);
			return base.LogAsync(context, cancellationToken);

		}
	}
}