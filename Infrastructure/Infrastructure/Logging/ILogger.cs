﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.Logging
{
    using System;
    using ColossalFramework.Plugins;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Logs a message with the type of <see cref="PluginManager.MessageType.Error"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional list of string formatting arguments for the message.</param>
        [StringFormatMethod("message")]
        void Error(string message, params object[] args);

        /// <summary>
        /// Logs a message with the type of <see cref="PluginManager.MessageType.Message"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional list of string formatting arguments for the message.</param>
        [StringFormatMethod("message")]
        void Info(string message, params object[] args);

        /// <summary>
        /// Logs a message with the specified message type.
        /// </summary>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional list of string formatting arguments for the message.</param>
        [StringFormatMethod("message")]
        void Log(PluginManager.MessageType messageType, string message, params object[] args);

        /// <summary>
        /// Logs an exception with the specified message type.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional list of string formatting arguments for the message.</param>
        [StringFormatMethod("message")]
        void LogException(Exception exception, string message = null, params object[] args);

        /// <summary>
        /// Logs a message with the type of <see cref="PluginManager.MessageType.Warning"/>.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional list of string formatting arguments for the message.</param>
        [StringFormatMethod("message")]
        void Warn(string message, params object[] args);
    }
}
