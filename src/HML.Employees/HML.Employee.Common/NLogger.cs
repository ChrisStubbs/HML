using System;
using NLog;
using ILogger = HML.Employee.Common.Interfaces.ILogger;

namespace HML.Employee.Common
{
	public class NLogger : ILogger
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public void LogDebug(string message)
		{
			this._logger.Debug(message);
		}

		public void LogError(string message)
		{
			this._logger.Error(message);
		}

		public void LogError(string message, Exception exception)
		{
			this._logger.Error(exception, message);

		}

		public void LogInfo(string message)
		{
			this._logger.Info(message);
		}

		public void LogWarn(string message)
		{
			this._logger.Warn(message);
		}
	}
}