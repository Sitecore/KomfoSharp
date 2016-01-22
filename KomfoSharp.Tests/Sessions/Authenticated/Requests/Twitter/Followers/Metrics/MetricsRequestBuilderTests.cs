// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class MetricsRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithExpectedConfiguration()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        }
      };

      configurationFactory.GetConfiguration().Returns(config);

      var expectedConfiguration = new MetricsRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Fields = MetricFields.Channel | MetricFields.Engagement,
        Polling =
        {
          Enabled = true,
          Interval = TimeSpan.FromSeconds(15),
          Attempts = 4
        }
      };

      // act
      var metricsRequest = new MetricsRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .Fields(expectedConfiguration.Fields)
        .WithPolling(polling => polling.Interval(expectedConfiguration.Polling.Interval).Attempts(expectedConfiguration.Polling.Attempts))
        .Create();

      // assert
      metricsRequest.Should().NotBeNull();
      metricsRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateRequestWithDefaultConfiguration()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var expectedConfiguration = new MetricsRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Fields = MetricFields.All,
        Polling =
        {
          Enabled = false
        }
      };

      // act
      var metricsRequest = new MetricsRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .Create();

      // assert
      metricsRequest.Should().NotBeNull();
      metricsRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateRequestWithDefaultPollingConfiguration()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var config = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15)
        }
      };

      configurationFactory.GetConfiguration().Returns(config);

      var expectedConfiguration = new MetricsRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Fields = MetricFields.All,
        Polling =
        {
          Enabled = true,
          Interval = config.PollingConfiguration.DefaultTimeInterval,
          Attempts = config.PollingConfiguration.DefaultAttemptsCount
        }
      };

      // act
      var metricsRequest = new MetricsRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .WithPolling()
        .Create();

      // assert
      metricsRequest.Should().NotBeNull();
      metricsRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }
  }
}