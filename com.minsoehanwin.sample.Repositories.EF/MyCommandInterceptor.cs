using log4net;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    /// <summary>
    /// Not using this class just for sample
    /// see more at http://www.tutorialspoint.com/entity_framework/entity_framework_command_interception.htm
    /// </summary>
    public class MyCommandInterceptor : IDbCommandInterceptor
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Log(string comm, string message)
        {
            _logger.Debug(string.Format("Intercepted: {0}, Command Text: {1} ", comm, message));
        }

        public void NonQueryExecuted(DbCommand command,
           DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuted: ", command.CommandText);
        }

        public void NonQueryExecuting(DbCommand command,
           DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuting: ", command.CommandText);
        }

        public void ReaderExecuted(DbCommand command,
           DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log("ReaderExecuted: ", command.CommandText);
        }

        public void ReaderExecuting(DbCommand command,
           DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log("ReaderExecuting: ", command.CommandText);
        }

        public void ScalarExecuted(DbCommand command,
           DbCommandInterceptionContext<object> interceptionContext)
        {
            Log("ScalarExecuted: ", command.CommandText);
        }

        public void ScalarExecuting(DbCommand command,
           DbCommandInterceptionContext<object> interceptionContext)
        {
            Log("ScalarExecuting: ", command.CommandText);
        }

    }

}
