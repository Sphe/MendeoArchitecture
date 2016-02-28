﻿using System;
using Common.Logging;
using Castle.Windsor;

namespace Mendeo.Scheduler.Core
{
    /// <summary>
    /// Factory class to create Quartz server implementations from.
    /// </summary>
    public class QuartzServerFactory
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(QuartzServerFactory));

        /// <summary>
        /// Creates a new instance of an Quartz.NET server core.
        /// </summary>
        /// <returns></returns>
        public static IQuartzServer CreateServer()
        {
            string typeName = Configuration.ServerImplementationType;

            Type t = Type.GetType(typeName, true);

            logger.Debug("Creating new instance of server type '" + typeName + "'");
            IQuartzServer retValue = (IQuartzServer)Activator.CreateInstance(t);
            logger.Debug("Instance successfully created");

            return retValue;
        }
    }
}
