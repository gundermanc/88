//----------------------------------------------------------------------- 
// <copyright file="Exception88.cs" company="Gundersoft"> 
//     Copyright (c) Christian Gunderman. All rights reserved. 
// </copyright> 
// <author>Christian Gunderman</author>
//-----------------------------------------------------------------------

namespace Uniscript
{
    using System;

    /// <summary>
    /// The parent class for all publicly emitted exceptions that occur under
    /// normal operation.
    /// </summary>
    public abstract class Exception88 : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Exception88"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception that caused this exception.</param>
        protected Exception88(string message = null, Exception inner = null) : base(message, inner)
        {
        }
    }
}