// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomAudienceToCampaignRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class AddCustomAudienceToCampaignRequestBuilderTests
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
      var addCustomAudienceRequestBuilder = new AddCustomAudienceToCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var addCustomAudienceRequest = addCustomAudienceRequestBuilder.Create();

      // assert
      addCustomAudienceRequest.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      addCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      addCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeFalse();
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
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0)
        }
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var addCustomAudienceRequestBuilder = new AddCustomAudienceToCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var addCustomAudienceRequest = addCustomAudienceRequestBuilder.WithPolling().Create();

      // assert
      addCustomAudienceRequest.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      addCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      addCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeTrue();
      addCustomAudienceRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      addCustomAudienceRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
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
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0)
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
      var addCustomAudienceRequestBuilder = new AddCustomAudienceToCampaignRequestBuilder(configurationProvider, campaignId, updateCustomAudiencesConfiguration);

      var addCustomAudienceRequest = addCustomAudienceRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      addCustomAudienceRequest.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CampaignId.Should().Be(campaignId);
      addCustomAudienceRequest.Configuration.CustomAudiences.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.CustomAudiences.ShouldBeEquivalentTo(updateCustomAudiencesConfiguration);
      addCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}
