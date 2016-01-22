// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceRequestBuilderTests.cs" company="Sitecore A/S">
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
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CustomAudienceRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();

      // act
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceRequest = customAudienceRequestBuilder.Create();

      // assert
      customAudienceRequest.Should().NotBeNull();
      customAudienceRequest.Configuration.Should().NotBeNull();
      customAudienceRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
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
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceRequest = customAudienceRequestBuilder.WithPolling().Create();

      // assert
      customAudienceRequest.Should().NotBeNull();
      customAudienceRequest.Configuration.Should().NotBeNull();
      customAudienceRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceRequest.Configuration.Polling.Enabled.Should().BeTrue();
      customAudienceRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      customAudienceRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceId = Guid.NewGuid().ToString();
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
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, customAudienceId);

      var customAudienceRequest = customAudienceRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      customAudienceRequest.Should().NotBeNull();
      customAudienceRequest.Configuration.Should().NotBeNull();
      customAudienceRequest.Configuration.CustomAudienceId.Should().Be(customAudienceId);
      customAudienceRequest.Configuration.Polling.Should().NotBeNull();
      customAudienceRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }

    [Test]
    public void ShouldCreateStatusRequestWithPassedCustomAudienceId()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var statusRequestConfiguration = new CustomAudienceStatusRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      // act
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, statusRequestConfiguration.CustomAudienceId);

      var customAudienceStatusRequest = customAudienceRequestBuilder.Status.Create();

      // assert
      customAudienceStatusRequest.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.Should().NotBeNull();
      customAudienceStatusRequest.Configuration.ShouldBeEquivalentTo(statusRequestConfiguration);
    }

    [Test]
    public void ShouldCreateAddUsersRequestWithPassedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users = 
        {
          Data = new[] { "user1@domain.com" },
          DataType = UpdateUsersInCustomAudienceDataType.Emails
        }
      };


      // act
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, updateUsersRequestConfiguration.CustomAudienceId);

      var addUsersRequest = customAudienceRequestBuilder
        .Users
        .Add(users => users.Emails(updateUsersRequestConfiguration.Users.Data))
        .Create();

      // assert
      addUsersRequest.Should().NotBeNull();
      addUsersRequest.Configuration.Should().NotBeNull();
      addUsersRequest.Configuration.ShouldBeEquivalentTo(updateUsersRequestConfiguration);
    }

    [Test]
    public void ShouldCreateRemoveUsersRequestWithPassedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var updateUsersRequestConfiguration = new UpdateUsersInCustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString(),
        Users =
        {
          Data = new[] { "user1@domain.com" },
          DataType = UpdateUsersInCustomAudienceDataType.Emails
        }
      };


      // act
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, updateUsersRequestConfiguration.CustomAudienceId);

      var removeUsersRequest = customAudienceRequestBuilder
        .Users
        .Remove(users => users.Emails(updateUsersRequestConfiguration.Users.Data))
        .Create();

      // assert
      removeUsersRequest.Should().NotBeNull();
      removeUsersRequest.Configuration.Should().NotBeNull();
      removeUsersRequest.Configuration.ShouldBeEquivalentTo(updateUsersRequestConfiguration);
    }

    [Test]
    public void ShouldCreateDeleteRequestWithPassedCustomAudienceId()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var deleteRequestConfiguration = new CustomAudienceDeleteRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      // act
      var customAudienceRequestBuilder = new CustomAudienceRequestBuilder(configurationProvider, deleteRequestConfiguration.CustomAudienceId);

      var customAudienceDeleteRequest = customAudienceRequestBuilder.Delete().Create();

      // assert
      customAudienceDeleteRequest.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.Should().NotBeNull();
      customAudienceDeleteRequest.Configuration.ShouldBeEquivalentTo(deleteRequestConfiguration);
    }
  }
}