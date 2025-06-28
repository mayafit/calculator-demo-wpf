/**
 * @fileoverview Main application entry point for the Calculator Demo WPF application
 * @module CalculatorDemo.App
 */

using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Windows;

namespace CalculatorDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Logger factory instance for the application
        /// </summary>
        public static ILoggerFactory LoggerFactory { get; private set; } = null!;

        /// <summary>
        /// Application startup event handler
        /// </summary>
        /// <param name="e">Startup event arguments</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Configure Serilog for structured logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logs/calculator-demo-.log", 
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            // Create Microsoft.Extensions.Logging logger factory
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            var logger = LoggerFactory.CreateLogger<App>();

            logger.LogInformation("Calculator Demo application starting up");

            try
            {
                base.OnStartup(e);
                logger.LogInformation("Application startup completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during application startup");
                throw;
            }
        }

        /// <summary>
        /// Application exit event handler
        /// </summary>
        /// <param name="e">Exit event arguments</param>
        protected override void OnExit(ExitEventArgs e)
        {
            var logger = LoggerFactory.CreateLogger<App>();
            logger.LogInformation("Calculator Demo application shutting down with exit code: {ExitCode}", e.ApplicationExitCode);
            
            // Flush and close Serilog
            Log.CloseAndFlush();
            
            base.OnExit(e);
        }
    }
} 