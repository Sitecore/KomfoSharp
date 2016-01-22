// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUsersRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class AddUsersToCustomAudienceRequestBuilderTests
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
      var addUsersRequestBuilder = new AddUsersToCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var addUsersRequest = addUsersRequestBuilder.Create();

      // assert
      addUsersRequest.Should().NotBeNull();
      addUsersRequest.Configuration.Should().NotBeNull();
      addUsersRequest.Configuration.Users.Should().NotBeNull();
      addUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      addUsersRequest.Configuration.Polling.Should().NotBeNull();
      addUsersRequest.Configuration.Polling.Enabled.Should().BeFalse();
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
      var addUsersRequestBuilder = new AddUsersToCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var addUsersRequest = addUsersRequestBuilder.WithPolling().Create();

      // assert
      addUsersRequest.Should().NotBeNull();
      addUsersRequest.Configuration.Should().NotBeNull();
      addUsersRequest.Configuration.Users.Should().NotBeNull();
      addUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      addUsersRequest.Configuration.Polling.Should().NotBeNull();
      addUsersRequest.Configuration.Polling.Enabled.Should().BeTrue();
      addUsersRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      addUsersRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
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
      var addUsersRequestBuilder = new AddUsersToCustomAudienceRequestBuilder(configurationProvider, customAudienceId, updateUsersConfiguration);

      var addUsersRequest = addUsersRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      addUsersRequest.Should().NotBeNull();
      addUsersRequest.Configuration.Should().NotBeNull();
      addUsersRequest.Configuration.Users.Should().NotBeNull();
      addUsersRequest.Configuration.Users.ShouldBeEquivalentTo(updateUsersConfiguration);
      addUsersRequest.Configuration.Polling.Should().NotBeNull();
      addUsersRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}