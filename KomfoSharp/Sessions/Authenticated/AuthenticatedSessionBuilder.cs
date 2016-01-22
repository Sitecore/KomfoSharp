// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedSessionBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated
{
  using System;
  using System.Linq;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.Authenticated.Fluent;
  using KomfoSharp.Sessions.Fluent;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;

  /// <summary>
  /// Defines the interfaces to build the authenticated session.
  /// </summary>
  public class AuthenticatedSessionBuilder : 
    IAuthenticatedSessionBuilder, 
    ITokenCalled, 
    IWithTokenRenewalCalled, 
    IClientIdCalled, 
    IClientSecretCalled,
    IScopesCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedSessionBuilder" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="komfoProvider">The Komfo provider.</param>
    public AuthenticatedSessionBuilder(IConfigurationProvider configurationProvider, IKomfoProvider komfoProvider)
    {
      this.ConfigurationProvider = configurationProvider;
      this.Configuration = new AuthenticatedSessionConfiguration();
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
    /// Gets the configuration of the session.
    /// </summary>
    /// <value>
    /// The configuration of the session.
    /// </value>
    protected AuthenticatedSessionConfiguration Configuration { get; private set; }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Specifies the <see cref="Token" /> in the current session builder.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    public ITokenCalled Token(Token token)
    {
      this.Configuration.Token = token;
      return this;
    }

    /// <summary>
    /// Enables the token renewal in the current session builder.
    /// </summary>
    /// <returns>
    /// The current session builder.
    /// </returns>
    IWithTokenRenewalCalled IWithTokenRenewalCalling.WithTokenRenewal()
    {
      this.Configuration.TokenRenewal.Enabled = true;
      return this;
    }

    /// <summary>
    /// Specifies the client ID in the current session builder.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    IClientIdCalled IClientIdCalling<IClientIdCalled>.ClientId(string clientId)
    {
      this.Configuration.TokenRenewal.ClientId = clientId;
      return this;
    }

    /// <summary>
    /// Specifies the client secret in the current session builder.
    /// </summary>
    /// <param name="clientSecret">The client secret.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    IClientSecretCalled IClientSecretCalling<IClientSecretCalled>.ClientSecret(string clientSecret)
    {
      this.Configuration.TokenRenewal.ClientSecret = clientSecret;
      return this;
    }

    /// <summary>
    /// Specifies the token scopes in the current session builder.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    IScopesCalled IScopesCalling<IScopesCalled>.Scopes(TokenScopes scopes)
    {
      this.Configuration.TokenRenewal.Scopes = JsonConvert.SerializeObject(scopes, new StringEnumConverter())
        .Trim(new[] { '"' })
        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToArray();

      return this;
    }

    /// <summary>
    /// Specifies the token scopes in the current session builder.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>
    /// The current session builder.
    /// </returns>
    IScopesCalled IScopesCalling<IScopesCalled>.Scopes(string[] scopes)
    {
      this.Configuration.TokenRenewal.Scopes = scopes;
      return this;
    }

    /// <summary>
    /// Creates the session.
    /// </summary>
    /// <returns>The session.</returns>
    IAuthenticatedSession ICreateCalling<IAuthenticatedSession>.Create()
    {
      return new AuthenticatedSession(this.ConfigurationProvider, this.Configuration, this.KomfoProvider);
    }
  }
}