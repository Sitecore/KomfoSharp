// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScopesCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Fluent
{
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the calls to specify token scopes.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface IScopesCalling<out T>
  {
    /// <summary>
    /// Specifies the token scopes.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>The result of the call.</returns>
    T Scopes(TokenScopes scopes);

    /// <summary>
    /// Specifies the token scopes.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>The result of the call.</returns>
    T Scopes(string[] scopes); 
  }
}