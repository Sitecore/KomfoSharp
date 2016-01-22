// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceDeleteRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CustomAudienceDeleteRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedCustomAudienceIdAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();

      // act
      var customAudienceDeleteRequestBuilder = new CustomAudienceDeleteRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceDeleteRequest = customAudienceDeleteRequestBuilder.Create();

      // assert
      customAudienceDeleteRequest.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceDeleteRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedCustomAudienceIdAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
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
      var customAudienceDeleteRequestBuilder = new CustomAudienceDeleteRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceDeleteRequest = customAudienceDeleteRequestBuilder.WithPolling().Create();

      // assert
      customAudienceDeleteRequest.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceDeleteRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Polling.Enabled.Should().BeTrue();
      customAudienceDeleteRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      customAudienceDeleteRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedCustomAudienceIdAndSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
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
      var customAudienceDeleteRequestBuilder = new CustomAudienceDeleteRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceDeleteRequest = customAudienceDeleteRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      customAudienceDeleteRequest.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceDeleteRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}