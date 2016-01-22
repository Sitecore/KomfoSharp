// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokensResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response for the tokens request.
  /// </summary>
  [Serializable]
  public class TokensResponse : ITokensResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TokensResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public TokensResponse(Token data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public Token Data { get; private set; }
  }
}