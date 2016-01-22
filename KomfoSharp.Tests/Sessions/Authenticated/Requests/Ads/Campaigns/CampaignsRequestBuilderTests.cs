// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignsRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CampaignsRequestBuilderTests
  {
    [Test]
    public void ShouldCreateCampaignsRequestWithDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      // act
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var campaignsRequest = campaignsRequestBuilder.Create();

      // assert
      campaignsRequest.Should().NotBeNull();
      campaignsRequest.Configuration.Should().NotBeNull();
      campaignsRequest.Configuration.Polling.Should().NotBeNull();
      campaignsRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateCampaignsRequestWithDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      
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
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var campaignsRequest = campaignsRequestBuilder.WithPolling().Create();

      // assert
      campaignsRequest.Should().NotBeNull();
      campaignsRequest.Configuration.Should().NotBeNull();
      campaignsRequest.Configuration.Polling.Should().NotBeNull();
      campaignsRequest.Configuration.Polling.Enabled.Should().BeTrue();
      campaignsRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      campaignsRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateCampaignsRequestWithSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

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
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var campaignsRequest = campaignsRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      campaignsRequest.Should().NotBeNull();
      campaignsRequest.Configuration.Should().NotBeNull();
      campaignsRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }

    [Test]
    public void ShouldCreateNewCampaignRequestWithBuiltData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var newCampaignRequestConfiguration = new NewCampaignRequestConfiguration
      {
        Campaign = 
        {
          ExtCampaignId = Guid.NewGuid().ToString(),
          ExtCampaignKey = Guid.NewGuid().ToString(),
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      // act
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var newCampaignRequest = campaignsRequestBuilder
        .New(campaign => campaign
          .ExternalCampaignKey(newCampaignRequestConfiguration.Campaign.ExtCampaignKey)
          .ExternalCampaignId(newCampaignRequestConfiguration.Campaign.ExtCampaignId)
          .Name(newCampaignRequestConfiguration.Campaign.Name)
          .Description(newCampaignRequestConfiguration.Campaign.Description))
        .Create();

      // assert
      newCampaignRequest.Should().NotBeNull();
      newCampaignRequest.Configuration.Should().NotBeNull();
      newCampaignRequest.Configuration.ShouldBeEquivalentTo(newCampaignRequestConfiguration);
    }

    [Test]
    public void ShouldCreateNewCampaignRequestWithPassedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var newCampaignRequestConfiguration = new NewCampaignRequestConfiguration
      {
        Campaign =
        {
          ExtCampaignId = Guid.NewGuid().ToString(),
          ExtCampaignKey = Guid.NewGuid().ToString(),
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      // act
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var newCampaignRequest = campaignsRequestBuilder
        .New(newCampaignRequestConfiguration.Campaign)
        .Create();

      // assert
      newCampaignRequest.Should().NotBeNull();
      newCampaignRequest.Configuration.Should().NotBeNull();
      newCampaignRequest.Configuration.ShouldBeEquivalentTo(newCampaignRequestConfiguration);
    }

    [Test]
    public void ShouldCreateCustomAudiencesRequestWithSpecifiedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudiencesRequestConfiguration = new CampaignCustomAudiencesRequestConfiguration
      {
        CampaignId = Guid.NewGuid().ToString()
      };

      // act
      var campaignsRequestBuilder = new CampaignsRequestBuilder(configurationProvider);

      var customAudiencesRequest = campaignsRequestBuilder
        .CampaignId(customAudiencesRequestConfiguration.CampaignId)
        .CustomAudiences
        .Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.ShouldBeEquivalentTo(customAudiencesRequestConfiguration);
    }
  }
}
