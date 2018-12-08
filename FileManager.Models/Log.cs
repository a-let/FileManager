﻿using System;

namespace FileManager.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public string LogLevel { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}