using log4net.ObjectRenderer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Helper
{
    /// <summary>
    /// http://www.adamtuliper.com/2012/12/logging-complete-objects-with-log4net.html
    /// </summary>
    public class Log4NetObjectLogger : IObjectRenderer
    {
        //private static string SEPEARATOR="|@@|";
        //private string MSGSEPEARATOR="|@@@|";
        public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
        {
            var ex = obj as Exception;

            //if its not an exception, dump it. If it's an exception, log extended details.
            if (ex == null)
            {
                //by default log up to 10 levels deep.
                ObjectDumper.Write(obj, 10, writer);
            }
            else
            {
                while (ex != null)
                {
                    RenderException(ex, writer);
                    ex = ex.InnerException;
                }
            }
        }

        private void RenderException(Exception ex, TextWriter writer)
        {
            writer.WriteLine(string.Format("Type: {0}", ex.GetType().FullName));
            writer.WriteLine(string.Format("Message: {0}", ex.Message));
            writer.WriteLine(string.Format("Source: {0}", ex.Source));
            writer.WriteLine(string.Format("TargetSite: {0}", ex.TargetSite));
            RenderExceptionData(ex, writer);
            writer.WriteLine(string.Format("StackTrace: {0}", ex.StackTrace));

            //writer.Write(string.Format("Type: {0}{1}"
            //    , string.IsNullOrEmpty(ex.GetType().FullName)?string.Empty
            //        : ex.GetType().FullName.Replace(System.Environment.NewLine,MSGSEPEARATOR)
            //    ,SEPEARATOR));
            //writer.Write(string.Format("Message: {0}{1}"
            //    ,string.IsNullOrEmpty(ex.Message)?string.Empty
            //        : ex.Message.Replace(System.Environment.NewLine, MSGSEPEARATOR)
            //,SEPEARATOR));
            //writer.Write(string.Format("Source: {0}{1}"
            //    , string.IsNullOrEmpty(ex.Source)?string.Empty
            //        : ex.Source.Replace(System.Environment.NewLine, MSGSEPEARATOR)
            //    ,SEPEARATOR));
            //writer.Write(string.Format("TargetSite: {0}{1}"
            //    , (ex.TargetSite==null)?string.Empty
            //        : ex.TargetSite.ToString().Replace(System.Environment.NewLine, MSGSEPEARATOR)
            //    ,SEPEARATOR));
            //RenderExceptionData(ex, writer);
            //writer.WriteLine(string.Format("StackTrace: {0}{1}"
            //    , string.IsNullOrEmpty(ex.StackTrace)?string.Empty
            //        : ex.StackTrace.Replace(System.Environment.NewLine, MSGSEPEARATOR)
            //    ,SEPEARATOR));
        }

        private void RenderExceptionData(Exception ex, TextWriter writer)
        {
            foreach (DictionaryEntry entry in ex.Data)
            {
                writer.WriteLine(string.Format("{0}: {1}", entry.Key, entry.Value));
                //writer.Write(string.Format("{0}: {1}{2}", entry.Key, entry.Value,SEPEARATOR));
            }
        }
    }

}