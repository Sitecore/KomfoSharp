// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokensRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using KomfoSharp.Sessions.Fluent;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens.Fluent;

  /// <summary>
  /// Defines the interfaces to build tokens request.
  /// </summary>
  public interface ITokensRequestBuilder : IClientIdCalling<IClientIdCalled>
  {
  }
}