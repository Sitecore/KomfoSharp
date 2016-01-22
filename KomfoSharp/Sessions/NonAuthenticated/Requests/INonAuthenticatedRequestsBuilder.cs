// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INonAuthenticatedRequestsBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests
{
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Fluent;

  /// <summary>
  /// Defines the methods that are used to build non-authenticated requests.
  /// </summary>
  public interface INonAuthenticatedRequestsBuilder : IOAuth20Calling
  {
  }
}