﻿using System;
using Serilog;
using Serilog.Events;
using SerilogWeb.Classic;

namespace SerilogWeb.Test
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // ReSharper disable once PossibleNullReferenceException
            SerilogWebClassic.Configure(cfg => cfg
                    .IgnoreRequestsMatching(ctx => ctx.Request.Url.PathAndQuery.StartsWith("/__browserLink"))
                    .EnableFormDataLogging(formData => formData
                                                .AtLevel(LogEventLevel.Debug)
                                                .OnMatch(ctx => ctx.Response.StatusCode >= 400))
                );
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
        }
    }
}