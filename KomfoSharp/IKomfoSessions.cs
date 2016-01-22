// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKomfoSessions.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp
{
  using KomfoSharp.Sessions.Authenticated;
  using KomfoSharp.Sessions.NonAuthenticated;

  /// <summary>
  /// Defines the methods that are used to build Komfo sessions.
  /// </summary>
  public interface IKomfoSessions
  {
    /// <summary>
    /// Creates the authenticated session builder.
    /// </summary>
    /// <returns>The authenticated session builder.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   // create requests inside the session
    /// }
    /// </code>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .WithTokenRenewal().ClientId("&lt;your_client_id&gt;").ClientSecret("&lt;your_client_secret&gt;").Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising)
    ///   .Create())
    /// {
    ///   // create requests inside the session
    /// }
    /// </code>
    /// </example>
    IAuthenticatedSessionBuilder Authenticated { get; }

    /// <summary>
    /// Creates the non-authenticated session builder.
    /// </summary>
    /// <returns>The non-authenticated session builder.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .NonAuthenticated
    ///   .Create())
    /// {
    ///   // create requests inside the session
    /// }
    /// </code>
    /// </example>
    INonAuthenticatedSessionBuilder NonAuthenticated { get; }
  }
}