// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokensRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using System;

  /// <summary>
  /// Represents the tokens request.
  /// </summary>
  [Serializable]
  public class TokensRequest : ITokensRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TokensRequest"/> class.
    /// </summary>
    public TokensRequest()
    {
      this.Configuration = new TokensRequestConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public TokensRequestConfiguration Configuration { get; private set; }
  }
}