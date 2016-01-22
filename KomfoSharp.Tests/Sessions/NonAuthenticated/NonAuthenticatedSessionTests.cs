// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonAuthenticatedSessionTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.NonAuthenticated
{
  using System;
  using System.Threading.Tasks;
  using FluentAssertions;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Provider;
  using KomfoSharp.Sessions.NonAuthenticated;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class NonAuthenticatedSessionTests
  {
    [Test]
    public async void ShouldPassExpectedParametersToKomfoProviderAndReturnResponse()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var clientId = Guid.NewGuid().ToString();
      var clientSecret = Guid.NewGuid().ToString();
      var scopes = new[] { Guid.NewGuid().ToString() };
      var token = new Token
      {
        AccessToken = Guid.NewGuid().ToString(),
        ExpiresIn = TimeSpan.FromDays(60)
      };

      komfoProvider.RetrieveAccessTokenAsync(clientId, clientSecret, scopes).Returns(Task.FromResult(token));

      // act
      using (var komfoSession = new NonAuthenticatedSession(configurationFactory, komfoProvider))
      {
        var tokensRequest = komfoSession.Requests.OAuth20.Tokens
          .ClientId(clientId)
          .ClientSecret(clientSecret)
          .Scopes(scopes)
          .Create();

        await komfoSession.ExecuteAsync(tokensRequest).ContinueWith(task =>
        {
          // assert
          komfoProvider.Received(1).RetrieveAccessTokenAsync(Arg.Is(clientId), Arg.Is(clientSecret), Arg.Do<string[]>(strings => strings.ShouldAllBeEquivalentTo(scopes)));
          task.Result.Should().NotBeNull();
          task.Result.Data.ShouldBeEquivalentTo(token);
        });
      }
    }
  }
}