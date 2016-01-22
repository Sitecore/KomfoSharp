// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WithPollingBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;

  /// <summary>
  /// Defines the interfaces to build the request with polling.
  /// </summary>
  public class WithPollingBuilder : IWithPollingBuilder, IIntervalCalled, IAttemptsCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WithPollingBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public WithPollingBuilder(IConfigurationProvider configurationProvider)
    {
      var defaultPollingConfiguration = configurationProvider.GetConfiguration().PollingConfiguration;
      
      this.PollingConfiguration = new PollingRequestConfiguration
      {
        Enabled = true,
        Interval = defaultPollingConfiguration.DefaultTimeInterval,
        Attempts = defaultPollingConfiguration.DefaultAttemptsCount
      };
    }

    /// <summary>
    /// Gets the polling configuration.
    /// </summary>
    /// <value>
    /// The polling configuration.
    /// </value>
    protected PollingRequestConfiguration PollingConfiguration { get; private set; }

    /// <summary>
    /// Specifies the polling interval in the current request.
    /// </summary>
    /// <param name="interval">The interval.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    public IIntervalCalled Interval(TimeSpan interval)
    {
      this.PollingConfiguration.Interval = interval;
      return this;
    }

    /// <summary>
    /// Specifies the attempts count in the current request.
    /// </summary>
    /// <param name="attemptsCount">The attempts count.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IAttemptsCalled IAttemptsCalling.Attempts(int attemptsCount)
    {
      this.PollingConfiguration.Attempts = attemptsCount;
      return this;
    }

    /// <summary>
    /// Creates the polling configuration.
    /// </summary>
    /// <returns>
    /// The polling configuration.
    /// </returns>
    public PollingRequestConfiguration Create()
    {
      return this.PollingConfiguration;
    }
  }
}