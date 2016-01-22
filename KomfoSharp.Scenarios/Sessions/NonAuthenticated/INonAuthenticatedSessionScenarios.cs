// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INonAuthenticatedSessionScenarios.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Scenarios.Sessions.NonAuthenticated
{
  using System;
  using System.Threading.Tasks;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.NonAuthenticated;
  using KomfoSharp.Sessions.NonAuthenticated.Requests.OAuth20.Tokens;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class INonAuthenticatedSessionScenarios : BaseScenario
  {
    [Test]
    public async void ExecuteAsyncTokensRequestScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var fakeSession = Substitute.For<INonAuthenticatedSession>();
      var fakeRequest = Substitute.For<ITokensRequest>();
      var fakeResponse = Substitute.For<ITokensResponse>();

      komfoSessions
        .NonAuthenticated
        .Create()
        .Returns(fakeSession);

      fakeSession.Requests.OAuth20.Tokens
        .ClientId(Arg.Any<string>())
        .ClientSecret(Arg.Any<string>())
        .Scopes(Arg.Any<TokenScopes>())
        .Create()
        .Returns(fakeRequest);

      fakeSession.ExecuteAsync(fakeRequest).Returns(Task.FromResult(fakeResponse));

      fakeResponse.Data.Returns(this.CreateFakeToken());

      // act
      using (var komfoSession = komfoSessions
        .NonAuthenticated
        .Create())
      {
        var tokensRequest = komfoSession.Requests.OAuth20.Tokens
          .ClientId("<your_client_id>")
          .ClientSecret("<your_client_secret>")
          .Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising)
          .Create();

        var tokensResponse = await komfoSession.ExecuteAsync(tokensRequest);

        Console.WriteLine("Access Token: {0}, expires in: {1} days.", tokensResponse.Data.AccessToken, tokensResponse.Data.ExpiresIn.TotalDays);
      }
    }
  }
}