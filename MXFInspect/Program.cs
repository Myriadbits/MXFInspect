#region license
//
// MXFInspect - Myriadbits MXF Viewer. 
// Inspect MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion

using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            var logPath = Path.GetDirectoryName(configFile);
            string txtLogFile = Path.Combine(logPath, "MXFInspect_log_.txt");
            string jsonLogFile = Path.Combine(logPath, "MXFInspect_log_.json");

            using var log = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            .WriteTo.File(new JsonFormatter(renderMessage: true), jsonLogFile)
            .WriteTo.File(txtLogFile,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] ({SourceContext}) <{ThreadId}:{ThreadName}> {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 80000000)

            .Destructure.ToMaximumDepth(5)
            .CreateLogger();

            Log.Logger = log;
            Log.ForContext(typeof(Program)).Information($"Application started from '{Application.ExecutablePath}'");
            Log.ForContext(typeof(Program)).Information($"Application Version: {Application.ProductVersion}");
            Log.ForContext(typeof(Program)).Information($"Operating System: {Environment.OSVersion}");
            Log.ForContext(typeof(Program)).Information($"Current Username: {Environment.UserName}, Computer Name: {Environment.MachineName}");
            Log.ForContext(typeof(Program)).Information($"Log path: '{logPath}'");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            Log.CloseAndFlush();
        }
    }
}
