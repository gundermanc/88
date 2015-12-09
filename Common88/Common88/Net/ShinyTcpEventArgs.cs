//-----------------------------------------------------------------------
// <copyright file="ShinyTcpEventArgs.cs">
//     Copyright (c) Christian Gunderman. All rights reserved.
// </copyright>
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Common88.Net
{
    using System;

    /// <summary>
    /// Event arguments for <see cref="ShinyTcpSocket"/> class.
    /// </summary>
    public class ShinyTcpEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="ShinyTcpSocket"/> that caused the event.
        /// </summary>
        private readonly ShinyTcpSocket sourceSocket;

        /// <summary>
        /// Creates a new instance of <see cref="ShinyTcpEventArgs"/> class.
        /// </summary>
        public ShinyTcpEventArgs(ShinyTcpSocket sourceSocket)
        {
            this.sourceSocket = sourceSocket;
        }

        /// <summary>
        /// The <see cref="ShinyTcpSocket"/> that caused the event.
        /// </summary>
        public ShinyTcpSocket SourceSocket
        {
            get
            {
                return this.sourceSocket;
            }
        }
    }
}
