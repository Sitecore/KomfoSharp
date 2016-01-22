// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenRenewalConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using System;

  /// <summary>
  /// Represents the token renewal configuration.
  /// </summary>
  [Serializable]
  public class TokenRenewalConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenRenewalConfiguration"/> class.
    /// </summary>
    public TokenRenewalConfiguration()
    {
      this.Enabled = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether token renewal is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the client identifier to perform the token renewal.
    /// </summary>
    /// <value>
    /// The client identifier.
    /// </value>
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret to perform the token renewal.
    /// </summary>
    /// <value>
    /// The client secret.
    /// </value>
    public string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the scopes.
    /// </summary>
    /// <value>
    /// The scopes.
    /// </value>
    public string[] Scopes { get; set; }
  }
}