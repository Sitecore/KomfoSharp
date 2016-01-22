// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonAuthenticatedRequestsBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests
{
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Fluent;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens.Fluent;

  /// <summary>
  /// Defines the methods that are used to build non-authenticated requests.
  /// </summary>
  public class NonAuthenticatedRequestsBuilder : INonAuthenticatedRequestsBuilder, IOAuth20Called
  {
    /// <summary>
    /// Gets the OAuth20 requests builder.
    /// </summary>
    /// <value>
    /// The OAuth20 requests builder.
    /// </value>
    public IOAuth20Called OAuth20
    {
      get
      {
        return this;
      }
    }

    /// <summary>
    /// Creates and gets tokens request builder.
    /// </summary>
    /// <value>
    /// The tokens request builder.
    /// </value>
    ITokensCalled ITokensCalling.Tokens
    {
      get
      {
        return new TokensRequestBuilder();
      }
    }
  }
}