// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientIdCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Fluent
{
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the interfaces to build the session with client ID specified.
  /// </summary>
  public interface IClientIdCalled : IClientSecretCalling<IClientSecretCalled>
  {
  }
}