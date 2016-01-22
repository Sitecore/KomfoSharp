// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomAudienceFromCampaignRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class RemoveCustomAudienceFromCampaignRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedDataAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();
      var updateCustomAudiencesConfiguration = new UpdateCustomAudiencesInCampaignConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      // act
      var removeCustomAudienceRequestBuilder = new RemoveCustomAudienceFromCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var removeCustomAudienceRequest = removeCustomAudienceRequestBuilder.Create();

      // assert
      removeCustomAudienceRequest.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      removeCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      removeCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedDataAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();
      var updateCustomAudiencesConfiguration = new UpdateCustomAudiencesInCampaignConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0),
          DefaultAttemptsCount = 4
        }
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var removeCustomAudienceRequestBuilder = new RemoveCustomAudienceFromCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var removeCustomAudienceRequest = removeCustomAudienceRequestBuilder.WithPolling().Create();

      // assert
      removeCustomAudienceRequest.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      removeCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      removeCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeTrue();
      removeCustomAudienceRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      removeCustomAudienceRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedDataAndSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();
      var updateCustomAudiencesConfiguration = new UpdateCustomAudiencesInCampaignConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0),
          DefaultAttemptsCount = 4
        }
      };

      var expectedPollingRequestConfiguration = new PollingRequestConfiguration
      {
        Enabled = true,
        Attempts = 6,
        Interval = TimeSpan.FromSeconds(10.0)
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var removeCustomAudienceRequestBuilder = new RemoveCustomAudienceFromCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var removeCustomAudienceRequest = removeCustomAudienceRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      removeCustomAudienceRequest.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      removeCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      removeCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}
