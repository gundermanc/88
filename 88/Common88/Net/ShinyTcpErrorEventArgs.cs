//-----------------------------------------------------------------------
// <copyright file="ShinyTcpErrorEventArgs.cs">
//     Copyright (c) Christian Gunderman. All rights reserved.
// </copyright>
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Common88.Net
{
    /// <summary>
    /// EventArgs for the <see cref="ShinyTcpSocket.Accept"/> event.
    /// </summary>
    public sealed class ShinyTcpErrorEventArgs : ShinyTcpEventArgs
    {
        /// <summary>
        /// The socket to the incoming connection.
        /// </summary>
        private readonly NetException exception;

        /// <summary>
        /// Creates a new instance of the <see cref="ShinyTcpErrorEventArgs"/> class.
        /// </summary>
        /// <param name="sourceSocket">The <see cref="ShinyTcpSocket"/> that caused the event.</param>
        /// <param name="exceptions">The <see cref="NetException"/> that caused the error.</param>
        internal ShinyTcpErrorEventArgs(ShinyTcpSocket sourceSocket, NetException exception)
            : base(sourceSocket)
        {
            this.exception = exception;
        }

        /// <summary>
        /// The <see cref="NetException"/>  that caused the error.
        /// </summary>
        public NetException AcceptSocket
        {
            get
            {
                return this.exception;
            }
        }
    }
}
