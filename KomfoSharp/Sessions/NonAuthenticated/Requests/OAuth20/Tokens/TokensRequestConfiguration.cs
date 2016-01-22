// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokensConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using System;

  /// <summary>
  /// Represents the tokens request configuration.
  /// </summary>
  [Serializable]
  public class TokensRequestConfiguration
  {
    /// <summary>
    /// Gets or sets the client identifier.
    /// </summary>
    /// <value>
    /// The client identifier.
    /// </value>
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret.
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