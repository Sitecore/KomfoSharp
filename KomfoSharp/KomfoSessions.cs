// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KomfoSessions.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp
{
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Configuration.Providers.AppConfig;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.Authenticated;
  using KomfoSharp.Sessions.NonAuthenticated;

  /// <summary>
  /// Defines the methods that are used to build Komfo sessions.
  /// </summary>
  public class KomfoSessions : IKomfoSessions
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoSessions"/> class.
    /// </summary>
    public KomfoSessions() :
      this(new AppConfigConfigurationProvider(), new KomfoProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoSessions" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="komfoProvider">The Komfo provider.</param>
    public KomfoSessions(IConfigurationProvider configurationProvider, IKomfoProvider komfoProvider)
    {
      this.ConfigurationProvider = configurationProvider;
      this.KomfoProvider = komfoProvider;
    }

    /// <summary>
    /// Gets the Komfo provider.
    /// </summary>
    /// <value>
    /// The Komfo provider.
    /// </value>
    protected IKomfoProvider KomfoProvider { get; private set; }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Creates the authenticated session builder.
    /// </summary>
    /// <returns>
    /// The authenticated session builder.
    /// </returns>
    /// <example>
    ///   <code>
    /// using (var komfoSession = komfoSessions
    ///   .Authenticated
    ///   .Token(token)
    ///   .Create())
    /// {
    ///   // create requests inside the session
    /// }
    /// </code>
    ///   <code>
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
    public IAuthenticatedSessionBuilder Authenticated
    {
      get
      {
        return new AuthenticatedSessionBuilder(this.ConfigurationProvider, this.KomfoProvider);
      }
    }

    /// <summary>
    /// Creates the non-authenticated session builder.
    /// </summary>
    /// <returns>
    /// The non-authenticated session builder.
    /// </returns>
    /// <example>
    ///   <code>
    /// using (var komfoSession = komfoSessions
    ///   .NonAuthenticated
    ///   .Create())
    /// {
    ///   // create requests inside the session
    /// }
    /// </code>
    /// </example>
    public INonAuthenticatedSessionBuilder NonAuthenticated
    {
      get
      {
        return new NonAuthenticatedSessionBuilder(this.ConfigurationProvider, this.KomfoProvider);
      }
    }
  }
}