// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class NewCampaignRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedCampaignConfigurationAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var newCampaignConfiguration = new Campaign
      {
        ExtCampaignId = Guid.NewGuid().ToString(),
        ExtCampaignKey = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString()
      };

      // act
      var newCampaignRequestBuilder = new NewCampaignRequestBuilder(configurationProvider, newCampaignConfiguration);

      var newCampaignRequest = newCampaignRequestBuilder.Create();

      // assert
      newCampaignRequest.Should().NotBeNull();
      newCampaignRequest.Configuration.Should().NotBeNull();
      newCampaignRequest.Configuration.Campaign.ShouldBeEquivalentTo(newCampaignConfiguration);
      newCampaignRequest.Configuration.Polling.Should().NotBeNull();
      newCampaignRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedCampaignConfigurationAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0)
        }
      };

      var newCampaign = new Campaign
      {
        ExtCampaignId = Guid.NewGuid().ToString(),
        ExtCampaignKey = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString()
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var newCampaignRequestBuilder = new NewCampaignRequestBuilder(configurationProvider, newCampaign);

      var newCampaignRequest = newCampaignRequestBuilder.WithPolling().Create();

      // assert
      newCampaignRequest.Should().NotBeNull();
      newCampaignRequest.Configuration.Should().NotBeNull();
      newCampaignRequest.Configuration.Campaign.ShouldBeEquivalentTo(newCampaign);
      newCampaignRequest.Configuration.Polling.Should().NotBeNull();
      newCampaignRequest.Configuration.Polling.Enabled.Should().BeTrue();
      newCampaignRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      newCampaignRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedCampaignConfigurationAndSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0)
        }
      };

      var newCampaignConfiguration = new Campaign
      {
        ExtCampaignId = Guid.NewGuid().ToString(),
        ExtCampaignKey = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Name = Guid.NewGuid().ToString()
      };

      var expectedPollingRequestConfiguration = new PollingRequestConfiguration
      {
        Attempts = 6,
        Interval = TimeSpan.FromSeconds(10.0),
        Enabled = true
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var newCampaignRequestBuilder = new NewCampaignRequestBuilder(configurationProvider, newCampaignConfiguration);

      var newCampaignRequest = newCampaignRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      newCampaignRequest.Should().NotBeNull();
      newCampaignRequest.Configuration.Should().NotBeNull();
      newCampaignRequest.Configuration.Campaign.ShouldBeEquivalentTo(newCampaignConfiguration);
      newCampaignRequest.Configuration.Polling.Should().NotBeNull();
      newCampaignRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}