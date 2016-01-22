// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokensResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response for the tokens request.
  /// </summary>
  public interface ITokensResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    Token Data { get; }
  }
}