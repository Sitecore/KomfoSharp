// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokensRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using System;
  using System.Linq;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Fluent;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens.Fluent;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;

  /// <summary>
  /// Defines the interfaces to build tokens request.
  /// </summary>
  public class TokensRequestBuilder : 
    ITokensRequestBuilder, 
    IClientIdCalled, 
    IClientSecretCalled, 
    IScopesCalled,
    ITokensCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TokensRequestBuilder"/> class.
    /// </summary>
    public TokensRequestBuilder()
    {
      this.TokensRequest = new TokensRequest();
    }

    /// <summary>
    /// Gets the tokens request.
    /// </summary>
    /// <value>
    /// The tokens request.
    /// </value>
    protected ITokensRequest TokensRequest { get; private set; }

    /// <summary>
    /// Specifies the client ID in the current request.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    public IClientIdCalled ClientId(string clientId)
    {
      this.TokensRequest.Configuration.ClientId = clientId;
      return this;
    }

    /// <summary>
    /// Specifies the client secret in the current request.
    /// </summary>
    /// <param name="clientSecret">The client secret.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IClientSecretCalled IClientSecretCalling<IClientSecretCalled>.ClientSecret(string clientSecret)
    {
      this.TokensRequest.Configuration.ClientSecret = clientSecret;
      return this;
    }

    /// <summary>
    /// Specifies the token scopes in the current request.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IScopesCalled IScopesCalling<IScopesCalled>.Scopes(TokenScopes scopes)
    {
      this.TokensRequest.Configuration.Scopes = JsonConvert.SerializeObject(scopes, new StringEnumConverter())
        .Trim(new[] { '"' })
        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToArray();

      return this;
    }

    /// <summary>
    /// Specifies the token scopes in the current request.
    /// </summary>
    /// <param name="scopes">The scopes.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IScopesCalled IScopesCalling<IScopesCalled>.Scopes(string[] scopes)
    {
      this.TokensRequest.Configuration.Scopes = scopes;
      return this;
    }

    /// <summary>
    /// Creates the tokens request.
    /// </summary>
    /// <returns>The tokens request.</returns>
    ITokensRequest ICreateCalling<ITokensRequest>.Create()
    {
      return this.TokensRequest;
    }
  }
}