// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INonAuthenticatedSessionBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated
{
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the methods that are used to build the non-authenticated session.
  /// </summary>
  public interface INonAuthenticatedSessionBuilder : ICreateCalling<INonAuthenticatedSession>
  {
  }
}