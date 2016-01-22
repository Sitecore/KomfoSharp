namespace KomfoSharp.Scenarios
{
  using KomfoSharp.Model;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class IKomfoSessionsScenarios
  {
    [Test]
    public void AuthenticatedSimpleScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var token = new Token();

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .Create())
      {
        // create requests inside the session
      }
    }

    [Test]
    public void AuthenticatedExtendedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();
      var token = new Token();

      // act
      using (var komfoSession = komfoSessions
        .Authenticated
        .Token(token)
        .WithTokenRenewal().ClientId("<your_client_id>").ClientSecret("<your_client_secret>").Scopes(TokenScopes.TwitterFollowers | TokenScopes.Advertising)
        .Create())
      {
        // create requests inside the session
      }
    }

    [Test]
    public void NonAuthenticatedScenario()
    {
      // arrange
      var komfoSessions = Substitute.For<IKomfoSessions>();

      // act
      using (var komfoSession = komfoSessions
        .NonAuthenticated
        .Create())
      {
        // create requests inside the session
      }
    }
  }
}