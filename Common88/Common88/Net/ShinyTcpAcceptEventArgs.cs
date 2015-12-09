//-----------------------------------------------------------------------
// <copyright file="ShinyTcpAcceptEventArgs.cs">
//     Copyright (c) Christian Gunderman. All rights reserved.
// </copyright>
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Common88.Net
{
    /// <summary>
    /// EventArgs for the <see cref="ShinyTcpSocket.Accept"/> event.
    /// </summary>
    public sealed class ShinyTcpAcceptEventArgs : ShinyTcpEventArgs
    {
        /// <summary>
        /// The socket to the incoming connection.
        /// </summary>
        private readonly ShinyTcpSocket acceptSocket;

        /// <summary>
        /// Creates a new instance of the <see cref="ShinyTcpAcceptEventArgs"/> class.
        /// </summary>
        /// <param name="sourceSocket">The <see cref="ShinyTcpSocket"/> that caused the event.</param>
        /// <param name="acceptSocket">The <see cref="ShinyTcpSocket"/> to the remote computer.</param>
        internal ShinyTcpAcceptEventArgs(ShinyTcpSocket sourceSocket, ShinyTcpSocket acceptSocket)
            : base(sourceSocket)
        {
            this.acceptSocket = acceptSocket;
        }

        /// <summary>
        /// The <see cref="ShinyTcpSocket"/> to the remote computer.
        /// </summary>
        public ShinyTcpSocket AcceptSocket
        {
            get
            {
                return this.acceptSocket;
            }
        }
    }
}
