// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceRequestBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class NewCustomAudienceRequestBuilderTests
  {
    [Test]
    public void ShouldCreateRequestWithPassedNewCustomAudienceConfigurationAndDisabledPolling()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var newCustomAudience = new CustomAudience
      {
        Name = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString()
      };

      // act
      var newCustomAudienceRequestBuilder = new NewCustomAudienceRequestBuilder(configurationProvider, newCustomAudience);

      var newCustomAudienceRequest = newCustomAudienceRequestBuilder.Create();

      // assert
      newCustomAudienceRequest.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.ShouldBeEquivalentTo(newCustomAudience);
      newCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeFalse();
    }

    [Test]
    public void ShouldCreateRequestWithPassedNewCustomAudienceConfigurationAndDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var newCustomAudience = new CustomAudience
      {
        Name = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString()
      };

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
      var newCustomAudienceRequestBuilder = new NewCustomAudienceRequestBuilder(configurationProvider, newCustomAudience);

      var newCustomAudienceRequest = newCustomAudienceRequestBuilder.WithPolling().Create();

      // assert
      newCustomAudienceRequest.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.ShouldBeEquivalentTo(newCustomAudience);
      newCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Polling.Enabled.Should().BeTrue();
      newCustomAudienceRequest.Configuration.Polling.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      newCustomAudienceRequest.Configuration.Polling.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateRequestWithPassedNewCustomAudienceConfigurationAndSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();
      var newCustomAudience = new CustomAudience
      {
        Name = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString()
      };

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
      var newCustomAudienceRequestBuilder = new NewCustomAudienceRequestBuilder(configurationProvider, newCustomAudience);

      var newCustomAudienceRequest = newCustomAudienceRequestBuilder
        .WithPolling(polling => polling
          .Interval(expectedPollingRequestConfiguration.Interval)
          .Attempts(expectedPollingRequestConfiguration.Attempts))
        .Create();

      // assert
      newCustomAudienceRequest.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.CustomAudience.ShouldBeEquivalentTo(newCustomAudience);
      newCustomAudienceRequest.Configuration.Polling.Should().NotBeNull();
      newCustomAudienceRequest.Configuration.Polling.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}