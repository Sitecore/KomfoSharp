// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonAuthenticatedSession.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated
{
  using System;
  using System.Threading.Tasks;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.NonAuthenticated.Requests;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens;

  /// <summary>
  /// Defines the methods that are used to manage non-authenticated requests.
  /// </summary>
  public class NonAuthenticatedSession : INonAuthenticatedSession
  {
    /// <summary>
    /// If <c>true</c> the current instance is already disposed. 
    /// </summary>
    private bool disposed;

    /// <summary>
    /// If <c>true</c> the Komfo provider should be disposed. 
    /// </summary>
    private readonly bool disposeKomfoProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="NonAuthenticatedSession" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public NonAuthenticatedSession(IConfigurationProvider configurationProvider)
      : this(configurationProvider, new KomfoProvider(configurationProvider))
    {
      this.disposeKomfoProvider = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NonAuthenticatedSession" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="komfoProvider">The Komfo provider.</param>
    public NonAuthenticatedSession(IConfigurationProvider configurationProvider, IKomfoProvider komfoProvider)
    {
      this.disposed = false;
      this.ConfigurationProvider = configurationProvider;
      this.KomfoProvider = komfoProvider;
    }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Gets the Komfo provider.
    /// </summary>
    /// <value>
    /// The Komfo provider.
    /// </value>
    protected IKomfoProvider KomfoProvider { get; private set; }

    /// <summary>
    /// Gets the non-authenticated requests builder.
    /// </summary>
    /// <value>
    /// The non-authenticated requests builder.
    /// </value>
    public INonAuthenticatedRequestsBuilder Requests
    {
      get
      {
        return new NonAuthenticatedRequestsBuilder();
      }
    }

    /// <summary>
    /// Executes the <see cref="ITokensRequest" /> asynchronously.
    /// </summary>
    /// <param name="tokensRequest">The tokens request.</param>
    /// <returns>
    /// The <see cref="Task{ITokensResponse}" />.
    /// </returns>
    /// <example>
    ///   <code>
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
    public async Task<ITokensResponse> ExecuteAsync(ITokensRequest tokensRequest)
    {
      var token = await this.KomfoProvider.RetrieveAccessTokenAsync(
        tokensRequest.Configuration.ClientId,
        tokensRequest.Configuration.ClientSecret,
        tokensRequest.Configuration.Scopes);

      return new TokensResponse(token);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
      {
        return;
      }

      if (disposing)
      {
        if (disposeKomfoProvider)
        {
          var provider = this.KomfoProvider as IDisposable;
          if (provider != null)
          {
            provider.Dispose();
          }
        }
      }

      this.disposed = true;
    }
  }
}