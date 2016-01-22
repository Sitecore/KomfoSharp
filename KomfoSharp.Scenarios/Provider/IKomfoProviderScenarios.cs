// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKomfoProviderScenarios.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Scenarios.Provider
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using KomfoSharp.Model;
  using KomfoSharp.Provider;
  using NSubstitute;
  using NUnit.Framework;
  using Ploeh.AutoFixture;

  [TestFixture]
  public class IKomfoProviderScenarios : BaseScenario
  {
    #region oauth20/tokens

    [Test]
    public async void RetrieveAccessTokenAsyncScenario()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.RetrieveAccessTokenAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string[]>()).Returns(Task.FromResult(this.CreateFakeToken()));

      // act
      var token = await komfoProvider.RetrieveAccessTokenAsync("<your_client_Id>", "<your_client_secret>", new[] { "twitter_followers" });

      Console.WriteLine("Access Token: {0}, expires in: {1} days", token.AccessToken, token.ExpiresIn.TotalDays);
    }

    #endregion

    #region twitter/followers

    [Test]
    public async void RetrieveMetricsAsyncScenario()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.RetrieveMetricsAsync(Arg.Any<string>(), Arg.Any<IEnumerable<string>>())
        .Returns(info => Task.FromResult(this.CreateFakeMetrics(info.Arg<IEnumerable<string>>(), "GeekFlavour")));

      // act
      var metrics = await komfoProvider.RetrieveMetricsAsync("<your_access_token>", new[] { "dkoroedova", "bkoroedova" });
      foreach (var metric in metrics)
      {
        this.ShowMetric(metric);
      }
    }

    [Test]
    public async void RetrieveTweetsAsyncScenario()
    {
      // arrange
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.RetrieveTweetsAsync(Arg.Any<string>(), Arg.Any<IEnumerable<string>>())
        .Returns(info => Task.FromResult(this.CreateFakeTweets(info.Arg<IEnumerable<string>>(), "GeekFlavour")));

      // act
      var tweets = await komfoProvider.RetrieveTweetsAsync("<your_access_token>", new[] { "dkoroedova", "bkoroedova" });
      foreach (var tweet in tweets)
      {
        this.ShowTweet(tweet);
      }
    }

    #endregion

    #region ads/customaudiences

    [Test]
    public async void RetrieveCustomAudiencesAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.RetrieveCustomAudiencesAsync("<your_access_token>").Returns(Task.FromResult(fixture.CreateMany<CustomAudience>()));

      // act
      var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("<your_access_token>");
      foreach (var customAudience in customAudiences)
      {
        this.ShowCustomAudience(customAudience);
      }
    }

    [Test]
    public async void RetrieveCustomAudiencesAsyncOverloadScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var campaignId = fixture.Create<string>();

      komfoProvider.RetrieveCustomAudiencesAsync("<your_access_token>", campaignId).Returns(Task.FromResult(fixture.CreateMany<CustomAudience>()));

      // act
      var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("<your_access_token>", campaignId);
      foreach (var customAudience in customAudiences)
      {
        this.ShowCustomAudience(customAudience);
      }
    }

    [Test]
    public async void RetrieveCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.RetrieveCustomAudienceAsync("<your_access_token>", customAudienceId).Returns(
        info => Task.FromResult(fixture.Build<CustomAudience>().With(x => x.Id, customAudienceId).Create()));

      // act
      try
      {
        var customAudience = await komfoProvider.RetrieveCustomAudienceAsync("<your_access_token>", customAudienceId);
        this.ShowCustomAudience(customAudience);
      }
      catch (KomfoProviderException ex)
      {
        if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
        {
          Console.WriteLine("The custom audience doesn't exist.");
        }
      }
    }

    [Test]
    public async void DeleteCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.DeleteCustomAudienceAsync("<your_access_token>", customAudienceId).Returns(
        info => Task.FromResult(fixture.Build<CustomAudience>().With(x => x.Id, customAudienceId).Create()));

      // act
      try
      {
        var customAudience = await komfoProvider.DeleteCustomAudienceAsync("<your_access_token>", customAudienceId);
        this.ShowCustomAudience(customAudience);
      }
      catch (KomfoProviderException ex)
      {
        if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
        {
          Console.WriteLine("The custom audience doesn't exist.");
        }

        if (ex.KomfoStatusCode == KomfoStatusCode.CustomAudienceParticipateInActiveAdvertisements)
        {
          Console.WriteLine("Cannot delete the custom audience. The custom audience is participating in active advertisements.");
        }
      }
    }

    [Test]
    public async void CreateCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.CreateCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(fixture.Create<string>()));

      // act
      var customAudienceId = await komfoProvider.CreateCustomAudienceAsync("<your_access_token>", "Sportsmen", "People who like sport");
      Console.WriteLine("Custom Audience ID: {0}", customAudienceId);
    }

    [Test]
    public async void AddEmailsToCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.AddEmailsToCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.AddEmailsToCustomAudienceAsync("<your_access_token>", customAudienceId, new[] { "user1@domain.com", "user2@domain.com", "user3@domain.com" });
      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void AddPhoneNumbersToCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.AddPhoneNumbersToCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.AddPhoneNumbersToCustomAudienceAsync("<your_access_token>", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void AddFacebookIdsToCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.AddFacebookIdsToCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<IEnumerable<string>>())
        .Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.AddFacebookIdsToCustomAudienceAsync(
        "<your_access_token>", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });

      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void RemoveEmailsFromCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.RemoveEmailsFromCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.RemoveEmailsFromCustomAudienceAsync("<your_access_token>", customAudienceId, new[] { "user1@domain.com", "user2@domain.com", "user3@domain.com" });
      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void RemovePhoneNumbersFromCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.RemovePhoneNumbersFromCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.RemovePhoneNumbersFromCustomAudienceAsync("<your_access_token>", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void RemoveFacebookIdsFromCustomAudienceAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.RemoveFacebookIdsFromCustomAudienceAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<IEnumerable<string>>())
        .Returns(Task.FromResult(fixture.Create<int>(1)));

      // act
      var entriesReceived = await komfoProvider.RemoveFacebookIdsFromCustomAudienceAsync(
        "<your_access_token>", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });

      Console.WriteLine("Entries received: {0}", entriesReceived);
    }

    [Test]
    public async void RetrieveCustomAudienceStatusAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var customAudienceId = fixture.Create<string>();

      komfoProvider.RetrieveCustomAudienceStatusAsync(Arg.Any<string>(), customAudienceId).Returns(
        Task.FromResult(fixture.Create<CustomAudienceStatus>()));

      // act
      try
      {
        var customAudienceStatus = await komfoProvider.RetrieveCustomAudienceStatusAsync("<your_access_token>", customAudienceId);
        this.ShowCustomAudienceStatus(customAudienceStatus);
      }
      catch (KomfoProviderException ex)
      {
        if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
        {
          Console.WriteLine("The custom audience doesn't exist or you don't have access to it.");
        }
      }
    }

    #endregion

    #region ads/campaigns

    [Test]
    public async void RetrieveCampaignsAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.RetrieveCampaignsAsync(Arg.Any<string>()).Returns(Task.FromResult(fixture.CreateMany<Campaign>()));

      // act
      var campaigns = await komfoProvider.RetrieveCampaignsAsync("<your_access_token>");
      foreach (var campaign in campaigns)
      {
        this.ShowCampaign(campaign);
      }
    }

    [Test]
    public async void CreateCampaignAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.CreateCampaignAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(fixture.Create<string>()));

      // act
      var campaignId = await komfoProvider.CreateCampaignAsync("<your_access_token>", "ABSDFFEHJDU0031239UEE", "ad_camp");
      Console.WriteLine("Campaign ID: {0}", campaignId);
    }

    [Test]
    public async void CreateCampaignAsyncOverloadScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();

      komfoProvider.CreateCampaignAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(fixture.Create<string>()));

      // act
      var campaignId = await komfoProvider.CreateCampaignAsync("<your_access_token>", "ABSDFFEHJDU0031239UEE", "ad_camp", "Summer 2015", "Targeted to coming summer 2015");
      Console.WriteLine("Campaign ID: {0}", campaignId);
    }

    [Test]
    public async void AddCustomAudienceToCampaignAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var campaignId = fixture.Create<string>();
      var customAudienceId = fixture.Create<string>();

      // act
      await komfoProvider.AddCustomAudienceToCampaignAsync("<your_access_token>", campaignId, customAudienceId);
    }

    [Test]
    public async void RemoveCustomAudienceFromCampaignAsyncScenario()
    {
      // arrange
      var fixture = new Fixture();
      var komfoProvider = Substitute.For<IKomfoProvider>();
      var campaignId = fixture.Create<string>();
      var customAudienceId = fixture.Create<string>();

      // act
      await komfoProvider.RemoveCustomAudienceFromCampaignAsync("<your_access_token>", campaignId, customAudienceId);
    }

    #endregion
  }
}