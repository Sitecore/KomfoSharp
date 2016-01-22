// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientSecretCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens.Fluent
{
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the interfaces to build a request with the client secret specified.
  /// </summary>
  public interface IClientSecretCalled : IScopesCalling<IScopesCalled>
  {
  }
}