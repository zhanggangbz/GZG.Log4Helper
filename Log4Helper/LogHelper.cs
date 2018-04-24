using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GZG.Log4Helper
{
    public class LogHelper
    {
        private static volatile LogHelper instance;
        private static readonly object obj = new object();

        public static LogHelper Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (obj)
                    {
                        if (null == instance)
                        {
                            instance = new LogHelper();
                        }
                    }

                }
                return instance;
            }
        }


        private log4net.ILog loginfo;
        LogHelper()
        {
            Setup();
            loginfo = log4net.LogManager.GetLogger("log4net");
        }

        private void Setup()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"Logs\EventLog.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 10;
            roller.MaximumFileSize = "1GB";
            roller.DatePattern = "(yyyyMMdd)";
            roller.RollingStyle = RollingFileAppender.RollingMode.Date;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        public void WriteLog(string info)
        {

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public void WriteInf(string info)
        {

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public void WriteError(string info)
        {

            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info);
            }
        }

        public void WriteWain(string info)
        {

            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(info);
            }
        }
    }
}
