// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokensRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.NonAuthenticated.Requests.OAuth20.Tokens
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens;
  using NUnit.Framework;

  [TestFixture]
  public class TokensRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithSpecifiedParameters()
    {
      // arrange
      var clientId = Guid.NewGuid().ToString();
      var clientSecret = Guid.NewGuid().ToString();
      var scopes = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

      // act
      var tokensRequestBuilder = new TokensRequestBuilder();

      var tokensRequest = tokensRequestBuilder
        .ClientId(clientId)
        .ClientSecret(clientSecret)
        .Scopes(scopes)
        .Create();

      // assert
      tokensRequest.Should().NotBeNull();
      tokensRequest.Configuration.ClientId.Should().Be(clientId);
      tokensRequest.Configuration.ClientSecret.Should().Be(clientSecret);
      tokensRequest.Configuration.Scopes.ShouldAllBeEquivalentTo(scopes);
    }
  }
}