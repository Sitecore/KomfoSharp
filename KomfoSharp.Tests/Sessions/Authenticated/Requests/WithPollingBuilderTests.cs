// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WithPollingBuilderTests.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Tests.Sessions.Authenticated.Requests
{
  using System;
  using FluentAssertions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class WithPollingBuilderTests
  {
    [Test]
    public void ShouldCreateDefaultPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0),
        }
      };

      configurationProvider.GetConfiguration().Returns(configuration);

      // act
      var withPollingBuilder = new WithPollingBuilder(configurationProvider);

      var pollingConfiguration = withPollingBuilder.Create();

      // assert
      pollingConfiguration.Enabled.Should().BeTrue();
      pollingConfiguration.Attempts.Should().Be(configuration.PollingConfiguration.DefaultAttemptsCount);
      pollingConfiguration.Interval.Should().Be(configuration.PollingConfiguration.DefaultTimeInterval);
    }

    [Test]
    public void ShouldCreateSpecifiedPollingConfiguration()
    {
      // arrange
      var configurationProvider = Substitute.For<IConfigurationProvider>();

      var configuration = new Configuration
      {
        PollingConfiguration = new PollingConfiguration
        {
          DefaultAttemptsCount = 4,
          DefaultTimeInterval = TimeSpan.FromSeconds(15.0),
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
      var withPollingBuilder = new WithPollingBuilder(configurationProvider);

      var pollingConfiguration = withPollingBuilder
        .Interval(expectedPollingRequestConfiguration.Interval)
        .Attempts(expectedPollingRequestConfiguration.Attempts)
        .Create();

      // assert
      pollingConfiguration.ShouldBeEquivalentTo(expectedPollingRequestConfiguration);
    }
  }
}