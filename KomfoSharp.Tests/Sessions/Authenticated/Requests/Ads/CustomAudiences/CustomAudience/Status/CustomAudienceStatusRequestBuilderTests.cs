// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceStatusRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CustomAudienceStatusRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedCustomAudienceIdAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();

      // act
      var customAudienceStatusRequestBuilder = new CustomAudienceStatusRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceStatusRequest = customAudienceStatusRequestBuilder.Create();

      // assert
      customAudienceStatusRequest.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceStatusRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Polling.Enabled.Should().BeFalse();
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
      var customAudienceStatusRequestBuilder = new CustomAudienceStatusRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceStatusRequest = customAudienceStatusRequestBuilder.WithPolling().Create();

      // assert
      customAudienceStatusRequest.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceStatusRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Polling.Enabled.Should().BeTrue();
      customAudienceStatusRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      customAudienceStatusRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
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
      var customAudienceStatusRequestBuilder = new CustomAudienceStatusRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceStatusRequest = customAudienceStatusRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      customAudienceStatusRequest.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceStatusRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}