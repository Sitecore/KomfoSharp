// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudiencesRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CampaignCustomAudiencesRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedCampaignIdAndDisabledPolling ()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();

      // act
      var customAudiencesRequestBuilder = new CampaignCustomAudiencesRequestBuilder(configurationProvider, campaignId);

      var customAudiencesRequest = customAudiencesRequestBuilder.Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.CampaignId.Should().Be(campaignId);
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedCampaignIdAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();
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
      var customAudiencesRequestBuilder = new CampaignCustomAudiencesRequestBuilder(configurationProvider, campaignId);

      var customAudiencesRequest = customAudiencesRequestBuilder.WithPolling().Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.CampaignId.Should().Be(campaignId);
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Enabled.Should().BeTrue();
      customAudiencesRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      customAudiencesRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedCampaignIdSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var campaignId = Guid.NewGuid().ToString();
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
      var customAudiencesRequestBuilder = new CampaignCustomAudiencesRequestBuilder(configurationProvider, campaignId);

      var customAudiencesRequest = customAudiencesRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.CampaignId.Should().Be(campaignId);
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }

    [Test]
    public void ShouldCreateAddCustomAudienceRequestWithSpecifiedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var updateCustomAudiencesRequestConfiguration = new UpdateCustomAudiencesInCampaignRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString(),
        CustomAudiences =
        {
          CustomAudienceId = Guid.NewGuid().ToString()
        }
      };
      
      // act
      var customAudiencesRequestBuilder = new CampaignCustomAudiencesRequestBuilder(configurationProvider, updateCustomAudiencesRequestConfiguration.CampaignId);

      var addCustomAudienceRequest = customAudiencesRequestBuilder
        .Add(customAudience => customAudience.CustomAudienceId(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId))
        .Create();

      // assert
      addCustomAudienceRequest.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.Should().NotBeNull();
      addCustomAudienceRequest.Configuration.ShouldBeEquivalentTo(updateCustomAudiencesRequestConfiguration);
    }

    [Test]
    public void ShouldCreateRemoveCustomAudienceRequestWithSpecifiedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var updateCustomAudiencesRequestConfiguration = new UpdateCustomAudiencesInCampaignRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString(),
        CustomAudiences =
        {
          CustomAudienceId = Guid.NewGuid().ToString()
        }
      };

      // act
      var customAudiencesRequestBuilder = new CampaignCustomAudiencesRequestBuilder(configurationProvider, updateCustomAudiencesRequestConfiguration.CampaignId);

      var removeCustomAudienceRequest = customAudiencesRequestBuilder
        .Remove(customAudience => customAudience.CustomAudienceId(updateCustomAudiencesRequestConfiguration.CustomAudiences.CustomAudienceId))
        .Create();

      // assert
      removeCustomAudienceRequest.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.Should().NotBeNull();
      removeCustomAudienceRequest.Configuration.ShouldBeEquivalentTo(updateCustomAudiencesRequestConfiguration);
    }
  }
}
