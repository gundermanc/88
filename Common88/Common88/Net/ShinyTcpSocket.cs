//-----------------------------------------------------------------------
// <copyright file="ShinyTcpSocket.cs">
//     Copyright (c) Christian Gunderman. All rights reserved.
// </copyright>
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Common88.Net
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Wraps standard Windows Universal <see cref="Socket"/> class and provides a more
    /// async and event driven friendly interface.
    /// </summary>
    public sealed class ShinyTcpSocket : IDisposable
    {
        /// <summary>
        /// A TCP protocol socket for outgoing or incoming connections.
        /// </summary>
        private readonly Socket socket;

        /// <summary>
        /// A <see cref="SocketAsyncEventArgs"/> object that will receive the results of
        /// asynchronous socket operations.
        /// </summary>
        private readonly SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();

        /// <summary>
        /// Creates a new listener socket and binds to the specified port.
        /// </summary>
        /// <param name="port">Port to listen on.</param>
        /// <param name="backlog">The max number of queued waiting connections.</param>
        /// <exception cref="NetException">A <see cref="SocketException"> occurred.</see></exception>
        public ShinyTcpSocket(int port, int backlog)
        {
            try
            {
                this.socketAsyncEventArgs.Completed += SocketAsyncEventArgs_Completed;

                this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                this.socket.Bind(new IPEndPoint(IPAddress.Any, port));
                this.socket.Listen(backlog);
            }
            catch (SocketException ex)
            {
                throw new NetException(string.Format("Unable to bind to port {0}.", port), ex);
            }
        }

        /// <summary>
        /// Creates a new ShinyTcpSocket from an accepted incoming connection.
        /// </summary>
        /// <param name="socket"></param>
        private ShinyTcpSocket(Socket socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// Listens asynchrononously for an incoming connection on this socket.
        /// When an incoming connection is detected, an <see cref="Accept"/> event
        /// is dispatched asynchronously.
        /// </summary>
        /// <exception cref="NetException">A <see cref="SocketException"> occurred.</see></exception>
        public void ListenAsync()
        {
            try
            {
                // If AcceptAsync returns false it indicates that the async operation
                // returned immediately, otherwise, the operation is pending and will
                // trigger an event.
                if (!this.socket.AcceptAsync(this.socketAsyncEventArgs))
                {
                    this.ProcessSocketEventArgs();
                }
            }
            catch (SocketException ex)
            {
                throw new NetException("Unable to accept incoming connection.", ex);
            }
        }

        /// <summary>
        /// Dispatched when an error occurs during an async call.
        /// </summary>
        public event EventHandler<ShinyTcpErrorEventArgs> Error;

        /// <summary>
        /// An outgoing <see cref="ShinyTcpSocket.ConnectAsync"/> call succeeded.
        /// </summary>
        public event EventHandler<ShinyTcpEventArgs> Connect;

        /// <summary>
        /// A listener <see cref="ShinyTcpSocket"/> received an incoming connection.
        /// </summary>
        public event EventHandler<ShinyTcpAcceptEventArgs> Accept;

        /// <summary>
        /// Frees the socket.
        /// </summary>
        public void Dispose()
        {
            this.socket.Dispose();
            this.socketAsyncEventArgs.Dispose();
        }

        /// <summary>
        /// Event handler callback routine for async operations on <see cref="socket"/>.
        /// </summary>
        /// <param name="sender">The object that dispatched the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessSocketEventArgs();
        }

        /// <summary>
        /// Processes the result of an async socket operation.
        /// </summary>
        private void ProcessSocketEventArgs()
        {
            // Dispatch an error event if the socket operation didn't complete successfully.
            if (this.socketAsyncEventArgs.SocketError != SocketError.Success)
            {
                if (this.Error != null)
                {
                    this.Error(this, new ShinyTcpErrorEventArgs(this, new NetException(
                        string.Format("Unable to complete {0} socket operation.",
                        this.socketAsyncEventArgs.SocketError.ToString()),
                        this.socketAsyncEventArgs.SocketError)));
                }

                return;
            }

            switch (this.socketAsyncEventArgs.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    if (this.Connect != null)
                    {
                        this.Connect(this, new ShinyTcpEventArgs(this));
                    }
                    break;

                case SocketAsyncOperation.Accept:
                    if (this.Accept != null)
                    {
                        this.Accept(this, new ShinyTcpAcceptEventArgs(
                            this, new ShinyTcpSocket(this.socketAsyncEventArgs.AcceptSocket)));
                    }
                    break;

                default:
                    // Indicates that we failed to account for a case.
                    throw new NotImplementedException("Unimplemented socket operation.");
            }
        }
    }
}
