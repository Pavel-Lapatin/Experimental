﻿using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Threading;

class CommonLogEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ProcessId", Process.GetCurrentProcess().Id));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ThreadId", Thread.CurrentThread.ManagedThreadId));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
               "StackTrace", Environment.StackTrace));
    }
}