// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INonAuthenticatedSession.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated
{
  using System;
  using System.Threading.Tasks;
  using KomfoSharp.Sessions.NonAuthenticated.Requests;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens;

  /// <summary>
  /// Defines the methods that are used to manage non-authenticated requests.
  /// </summary>
  public interface INonAuthenticatedSession : IDisposable
  {
    /// <summary>
    /// Gets the non-authenticated requests builder.
    /// </summary>
    /// <value>
    /// The non-authenticated requests builder.
    /// </value>
    INonAuthenticatedRequestsBuilder Requests { get; }

    /// <summary>
    /// Executes the <see cref="ITokensRequest"/> asynchronously.
    /// </summary>
    /// <param name="tokensRequest">The tokens request.</param>
    /// <returns>The <see cref="Task{ITokensResponse}"/>.</returns>
    /// <example>
    /// <code>
    /// using (var komfoSession = komfoSessions
    ///   .NonAuthenticated
    ///   .Create())
    /// {
    ///   var tokensRequest = komfoSession.Requests.OAuth20.Tokens
    ///     .ClientId("&lt;your_client_id&gt;")
    ///     .ClientSecret("&lt;your_client_secret&gt;")
    ///     .Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising)
    ///     .Create();
    ///
    ///   var tokensResponse = await komfoSession.ExecuteAsync(tokensRequest);
    ///
    ///   Console.WriteLine("Access Token: {0}, expires in: {1} days.", tokensResponse.Data.AccessToken, tokensResponse.Data.ExpiresIn.TotalDays);
    /// }
    /// </code>
    /// </example>
    Task<ITokensResponse> ExecuteAsync(ITokensRequest tokensRequest);
  }
}