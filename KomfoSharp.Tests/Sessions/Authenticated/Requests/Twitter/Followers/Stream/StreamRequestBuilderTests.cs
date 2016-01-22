// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class StreamRequestBuilderTests
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

      var expectedConfiguration = new StreamRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Fields = TweetFields.Channel | TweetFields.RequestHandle | TweetFields.Text,
        Since = DateTime.UtcNow.AddMonths(-1),
        Until = DateTime.UtcNow.AddDays(-1),
        Polling =
        {
          Enabled = true,
          Interval = TimeSpan.FromSeconds(15),
          Attempts = 4
        }
      };

      // act
      var streamRequest = new StreamRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .Fields(expectedConfiguration.Fields)
        .Since(expectedConfiguration.Since.Value)
        .Until(expectedConfiguration.Until.Value)
        .WithPolling(polling => polling.Interval(expectedConfiguration.Polling.Interval).Attempts(expectedConfiguration.Polling.Attempts))
        .Create();

      // assert
      streamRequest.Should().NotBeNull();
      streamRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }

    [Test]
    public void ShouldCreateRequestWithDefaultConfiguration()
    {
      // arrange
      var configurationFactory = Substitute.For<IConfigurationProvider>();
      var expectedConfiguration = new StreamRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Polling =
        {
          Enabled = false
        }
      };

      // act
      var streamRequest = new StreamRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .Create();

      // assert
      streamRequest.Should().NotBeNull();
      streamRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
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

      var expectedConfiguration = new StreamRequestConfiguration
      {
        TwitterHandles = new[] { "twh1", "twh2", "twh3" },
        Polling =
        {
          Enabled = true,
          Interval = config.PollingConfiguration.DefaultTimeInterval,
          Attempts = config.PollingConfiguration.DefaultAttemptsCount
        }
      };

      // act
      var streamRequest = new StreamRequestBuilder(configurationFactory)
        .TwitterHandles(expectedConfiguration.TwitterHandles)
        .WithPolling()
        .Create();

      // assert
      streamRequest.Should().NotBeNull();
      streamRequest.Configuration.ShouldBeEquivalentTo(expectedConfiguration);
    }
  }
}