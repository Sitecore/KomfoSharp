// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the base request builder.
  /// </summary>
  public abstract class BaseRequestBuilder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    protected BaseRequestBuilder(IConfigurationProvider configurationProvider)
    {
      this.ConfigurationProvider = configurationProvider;
    }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Builds the polling request configuration.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>The polling request configuration.</returns>
    protected virtual PollingRequestConfiguration BuildPollingRequestConfiguration(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      var withPollingBuilder = new WithPollingBuilder(this.ConfigurationProvider);

      return pollingSetupFunc == null ? withPollingBuilder.Create() : pollingSetupFunc(withPollingBuilder).Create();
    }
  }
}