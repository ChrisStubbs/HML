using System;
using NLog;

namespace HML.Immunisation.Common
{
	public class NLogger : Interfaces.ILogger
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