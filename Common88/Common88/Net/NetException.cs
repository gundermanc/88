//-----------------------------------------------------------------------
// <copyright file="NetException.cs">
//     Copyright (c) Christian Gunderman. All rights reserved.
// </copyright>
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Common88.Net
{
    using System.Net.Sockets;
    using Common88;

    /// <summary>
    /// The parent class for all publicly emitted networking exceptions that occur
    /// under normal operation.
    /// </summary>
    public sealed class NetException : Exception88
    {
        /// <summary>
        /// The <see cref="SocketError"/> that caused this exception.
        /// </summary>
        private readonly SocketError socketError;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception that caused this exception.</param>
        public NetException(string message, SocketException inner)
            : base(message, inner)
        {
            this.socketError = inner.SocketErrorCode;
        }

        /// <summary>
        /// Initializes a new instace of the <see cref="NetException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="socketError">The socketError that caused this exception.</param>
        public NetException(string message, SocketError socketError)
            : base(message)
        {
            this.socketError = socketError;
        }

        /// <summary>
        /// The <see cref="SocketException"/> that caused this <see cref="NetException"/>/>,
        /// or null if this exception was not caused by a SocketException.
        /// </summary>
        public SocketException SocketException
        {
            get
            {
                return this.InnerException as SocketException;
            }
        }

        /// <summary>
        /// Gets the <see cref="SocketError"/> responsible for this <see cref="NetException"/>.
        /// </summary>
        public SocketError SocketError
        {
            get
            {
                return this.socketError;
            }
        }
    }
}
