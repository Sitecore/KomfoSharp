// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokensRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  /// <summary>
  /// Defines the tokens request.
  /// </summary>
  public interface ITokensRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    TokensRequestConfiguration Configuration { get; }
  }
}