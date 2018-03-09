using System;

namespace HML.Employee.Common.Interfaces
{
	public interface ILogger
	{
		void LogDebug(string message);

		void LogError(string message);

		void LogError(string message, Exception exception);

		void LogInfo(string message);

		void LogWarn(string message);

	}
}