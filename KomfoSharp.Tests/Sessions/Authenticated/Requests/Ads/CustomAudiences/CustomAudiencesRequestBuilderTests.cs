// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudiencesRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class CustomAudiencesRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var customAudiencesRequest = customAudiencesRequestBuilder.Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithDefaultPollingConfiguration()
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

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var customAudiencesRequest = customAudiencesRequestBuilder.WithPolling().Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Enabled.Should().BeTrue();
      customAudiencesRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      customAudiencesRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithSpecifiedPollingConfiguration()
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

      var expectedPollingRequestConfiguration = new PollingRequestConfiguration
      {
        Enabled = true,
        Attempts = 6,
        Interval = TimeSpan.FromSeconds(10.0)
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var customAudiencesRequest = customAudiencesRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      customAudiencesRequest.Should().NotBeNull();
      customAudiencesRequest.Configuration.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.Should().NotBeNull();
      customAudiencesRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }

    [Test]
    public void ShouldCreateNewCustomAudienceRequestWithBuiltData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var newCustomAudienceRequestConfiguration = new NewCustomAudienceRequestConfiguration
      {
        CustomAudience = 
        {
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var newCustomAudienceRequest = customAudiencesRequestBuilder
        .New(customAudience => customAudience
          .Name(newCustomAudienceRequestConfiguration.CustomAudience.Name)
          .Description(newCustomAudienceRequestConfiguration.CustomAudience.Description))
        .Create();

      // assert
      newCustomAudienceRequest.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.ShouldBeEquivalentTo(newCustomAudienceRequestConfiguration);
    }

    [Test]
    public void ShouldCreateNewCustomAudienceRequestWithPassedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var newCustomAudienceRequestConfiguration = new NewCustomAudienceRequestConfiguration
      {
        CustomAudience =
        {
          Name = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString()
        }
      };

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var newCustomAudienceRequest = customAudiencesRequestBuilder
        .New(newCustomAudienceRequestConfiguration.CustomAudience)
        .Create();

      // assert
      newCustomAudienceRequest.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.ShouldBeEquivalentTo(newCustomAudienceRequestConfiguration);
    }

    [Test]
    public void ShouldCreateCustomAudienceRequestWithPassedData()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var customAudienceRequestConfiguration = new CustomAudienceRequestConfiguration
      {
        CustomAudienceId = Guid.NewGuid().ToString()
      };

      // act
      var customAudiencesRequestBuilder = new CustomAudiencesRequestBuilder(configurationProvider);

      var customAudienceRequest = customAudiencesRequestBuilder.CustomAudienceId(customAudienceRequestConfiguration.CustomAudienceId).Create();

      // assert
      customAudienceRequest.Should().NotBeNull();
      customAudienceRequest.Configuration.Should().NotBeNull();
      customAudienceRequest.Configuration.ShouldBeEquivalentTo(customAudienceRequestConfiguration);
    }
  }
}