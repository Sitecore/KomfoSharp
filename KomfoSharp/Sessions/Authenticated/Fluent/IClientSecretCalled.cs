// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientSecretCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Fluent
{
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the interfaces to build the session with client secret specified.
  /// </summary>
  public interface IClientSecretCalled : IScopesCalling<IScopesCalled>
  {
  }
}