// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticatedSessionBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.Authenticated;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class AuthenticatedSessionBuilderTests
  {
    [Test]
    public void ShouldCreateSessionWithExpectedConfiguration()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      configurationFactory.GetConfiguration().Returns(new Configuration());
      var expectedConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = true,
          ClientId = Guid.NewGuid().ToString(),
          ClientSecret = Guid.NewGuid().ToString()
        }
      };

      // act
      var builder = new AuthenticatedSessionBuilder(configurationFactory, komfoProvider);

      var session = builder
        .Token(expectedConfiguration.Token)
        .WithTokenRenewal().ClientId(expectedConfiguration.TokenRenewal.ClientId).ClientSecret(expectedConfiguration.TokenRenewal.ClientSecret).Scopes(expectedConfiguration.TokenRenewal.Scopes)
        .Create();

      // assert
      session.Should().NotBeNull();
      session.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateSessionWithDisabledTokenRenewal()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      configurationFactory.GetConfiguration().Returns(new Configuration());
      var expectedConfiguration = new AuthenticatedSessionConfiguration
      {
        Token =
        {
          AccessToken = Guid.NewGuid().ToString(),
          ExpiresIn = TimeSpan.FromDays(60)
        },
        TokenRenewal =
        {
          Enabled = false
        }
      };

      // act
      var builder = new AuthenticatedSessionBuilder(configurationFactory, komfoProvider);

      var session = builder
        .Token(expectedConfiguration.Token)
        .Create();

      // assert
      session.Should().NotBeNull();
      session.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }
  }
}