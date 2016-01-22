// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KomfoProviderException.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System;
  using System.Net;
  using System.Runtime.Serialization;

  /// <summary>
  /// The exception, which is thrown when an error occurs during a call to Komfo.
  /// </summary>
  [Serializable]
  public class KomfoProviderException : Exception
  {
    /// <summary>
    /// The default error message
    /// </summary>
    protected const string DefaultErrorMessage = "An error has occurred during the call to Komfo.";

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    public KomfoProviderException()
      : base(DefaultErrorMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public KomfoProviderException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public KomfoProviderException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="komfoStatusCode">The Komfo status code.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <param name="innerException">The inner exception.</param>
    public KomfoProviderException(string message, KomfoStatusCode komfoStatusCode, HttpStatusCode httpStatusCode, Exception innerException)
      : base(message, innerException)
    {
      this.KomfoStatusCode = komfoStatusCode;
      this.HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="komfoStatusCode">The Komfo status code.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    public KomfoProviderException(string message, KomfoStatusCode komfoStatusCode, HttpStatusCode httpStatusCode)
      : base(message)
    {
      this.KomfoStatusCode = komfoStatusCode;
      this.HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProviderException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected KomfoProviderException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.HttpStatusCode = (HttpStatusCode)info.GetValue("httpStatusCode", typeof(HttpStatusCode));
      this.KomfoStatusCode = (KomfoStatusCode)info.GetValue("komfoStatusCode", typeof(KomfoStatusCode));
    }

    /// <summary>
    /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    ///  </PermissionSet>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);

      info.AddValue("httpStatusCode", this.HttpStatusCode);
      info.AddValue("komfoStatusCode", this.KomfoStatusCode);
    }

    /// <summary>
    /// Gets or sets the Komfo status code.
    /// </summary>
    /// <value>
    /// The Komfo status code.
    /// </value>
    public KomfoStatusCode KomfoStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code.
    /// </summary>
    /// <value>
    /// The HTTP status code.
    /// </value>
    public HttpStatusCode HttpStatusCode { get; set; }
  }
}
