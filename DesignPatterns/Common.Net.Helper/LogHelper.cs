﻿using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Net.Helper
{
    /// <summary>
    /// 日志组件
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void Error(string message)
        {
            Logg.GetLogByName("ErrorFileAppender").Error(message);
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="message">异常类型</param>
        public static void Error(Exception message)
        {
            Logg.GetLogByName("ErrorFileAppender").Error(message.Message);
        }

        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            Logg.GetLogByName("DebugFileAppender").Debug(message);
        }

        /// <summary>
        /// 输出说明
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Logg.GetLogByName("DebugFileAppender").Info(message);
        }
    }

    //处理每一条日志委托方法 
    public delegate void LogHandler(string msg);

    /// <summary>
    /// 日志操作类
    /// 使用本类必须保证配置节上存在log4net的配置（web/app.config中）
    /// </summary>  
    public class Logg
    {
        /// <summary>
        /// 日志实例字典
        /// </summary>
        private static Dictionary<string, ILog> logbyConfig;
        /// <summary>
        /// 同步锁
        /// </summary>
        static object logbyConfiglock = new object();
        /// <summary>
        /// 当前日志实例
        /// </summary>
        private static ILog log;

        /// <summary>
        /// 日志记录实体类
        /// </summary>
        public static ILog Log
        {
            get { return Logg.log; }
            set { Logg.log = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configPath"></param>
        static Logg()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 加载日志记录配置
        /// </summary>
        /// <param name="configFilePath"></param>
        public static void Config(string configFilePath)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configFilePath));
        }

        /// <summary>
        /// 根据配置的log名称获取Log实例
        /// </summary>
        /// <param name="logname">log配置节点名称</param>
        /// <returns></returns>
        /// <remarks>每次调用都会创建一个实例，使用时考虑重复利用问题</remarks>
        public static ILog GetLogByName(string LogName = "LogInfo")
        {
            if (logbyConfig == null)
            {
                lock (logbyConfiglock)
                {
                    logbyConfig = new Dictionary<string, ILog>();
                }
            }
            if (!logbyConfig.ContainsKey(LogName))
            {
                lock (logbyConfiglock)
                {
                    if (!logbyConfig.ContainsKey(LogName))
                    {
                        ILog log = log4net.LogManager.GetLogger(LogName);
                        logbyConfig.Add(LogName, log);
                    }
                }
            }
            return logbyConfig[LogName];
        }

        /// <summary>
        /// 读取txt日志文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public static void ReadLog(string filepath, LogHandler delHandleLog)
        {
            if (!File.Exists(filepath))
                return;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filepath, System.Text.Encoding.UTF8);
                string EveryLineLog = string.Empty;

                while (!sr.EndOfStream)
                {
                    EveryLineLog = sr.ReadLine();

                    if (EveryLineLog != null)
                    {
                        delHandleLog(EveryLineLog);
                    }
                }
                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {

                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
    }
}
