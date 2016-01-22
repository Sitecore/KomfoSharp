// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedSessionConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the authenticated session configuration.
  /// </summary>
  [Serializable]
  public class AuthenticatedSessionConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedSessionConfiguration"/> class.
    /// </summary>
    public AuthenticatedSessionConfiguration()
    {
      this.Token = new Token();
      this.TokenRenewal = new TokenRenewalConfiguration();
    }

    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    /// <value>
    /// The token.
    /// </value>
    public Token Token { get; set; }

    /// <summary>
    /// Gets or sets the token renewal configuration.
    /// </summary>
    /// <value>
    /// The token renewal configuration.
    /// </value>
    public TokenRenewalConfiguration TokenRenewal { get; set; }
  }
}