// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticatedSessionBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using KomfoSharp.Sessions.Authenticated.Fluent;

  /// <summary>
  /// Defines the interfaces to build the authenticated session.
  /// </summary>
  public interface IAuthenticatedSessionBuilder : ITokenCalling
  {
  }
}