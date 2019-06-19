﻿using FileManager.Web.Services.Interfaces;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface ILog
    {
        bool IsEnabled { get; }

        void Log(LogLevel logLevel, Exception exception, Func<Exception, string> formatter);

        Task LogAsync(LogLevel logLevel, Exception exception, Func<Exception, string> formatter);
    }
}