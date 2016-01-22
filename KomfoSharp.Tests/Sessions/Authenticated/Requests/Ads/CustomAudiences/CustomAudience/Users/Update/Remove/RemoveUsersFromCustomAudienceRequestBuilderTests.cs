// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveUsersFromCustomAudienceRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Remove;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class RemoveUsersFromCustomAudienceRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedDataAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
      var updateUsersConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        FacebookApplicationsIds = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
      };

      // act
      var removeUsersRequestBuilder = new RemoveUsersFromCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var removeUsersRequest = removeUsersRequestBuilder.Create();

      // assert
      removeUsersRequest.Should().NotBeNull();
      removeUsersRequest.Configuration.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      removeUsersRequest.Configuration.Polling.Should().NotBeNull();
      removeUsersRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedDataAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
      var updateUsersConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        FacebookApplicationsIds = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
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
      var removeUsersRequestBuilder = new RemoveUsersFromCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var removeUsersRequest = removeUsersRequestBuilder.WithPolling().Create();

      // assert
      removeUsersRequest.Should().NotBeNull();
      removeUsersRequest.Configuration.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      removeUsersRequest.Configuration.Polling.Should().NotBeNull();
      removeUsersRequest.Configuration.Polling.Enabled.Should().BeTrue();
      removeUsersRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      removeUsersRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedDataAndSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
      var updateUsersConfiguration = new UpdateUsersInCustomAudienceConfiguration
      {
        Data = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
        FacebookApplicationsIds = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
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
      var removeUsersRequestBuilder = new RemoveUsersFromCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var removeUsersRequest = removeUsersRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      removeUsersRequest.Should().NotBeNull();
      removeUsersRequest.Configuration.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.Should().NotBeNull();
      removeUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      removeUsersRequest.Configuration.Polling.Should().NotBeNull();
      removeUsersRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}