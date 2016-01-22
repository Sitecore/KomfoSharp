// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the methods to build the metrics request.
  /// </summary>
  public class MetricsRequestBuilder : 
    BaseRequestBuilder,
    IMetricsCalled,
    IMetricsRequestBuilder,
    ITwitterHandlesCalled,
    IFieldsCalled,
    IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsRequestBuilder" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public MetricsRequestBuilder(IConfigurationProvider configurationProvider)
      : base(configurationProvider)
    {
      this.MetricsRequest = new MetricsRequest();
    }

    /// <summary>
    /// Gets the metrics request.
    /// </summary>
    /// <value>
    /// The metrics request.
    /// </value>
    protected MetricsRequest MetricsRequest { get; private set; }

    /// <summary>
    /// Specifies the Twitter handles in the current request.
    /// </summary>
    /// <param name="twitterHandles">The Twitter handles.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    public ITwitterHandlesCalled TwitterHandles(IEnumerable<string> twitterHandles)
    {
      this.MetricsRequest.Configuration.TwitterHandles = twitterHandles;
      return this;
    }

    /// <summary>
    /// Specify the metric fields to be returned in the current request.
    /// </summary>
    /// <param name="fields">The metric fields to be returned.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IFieldsCalled IFieldsCalling<IFieldsCalled, MetricFields>.Fields(MetricFields fields)
    {
      this.MetricsRequest.Configuration.Fields = fields;
      return this;
    }

    /// <summary>
    /// Enables the polling in the current request.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The current request builder.
    /// </returns>
    IWithPollingCalled IWithPollingCalling<IWithPollingCalled>.WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc)
    {
      this.MetricsRequest.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the request.
    /// </summary>
    /// <returns>The request.</returns>
    IMetricsRequest ICreateCalling<IMetricsRequest>.Create()
    {
      return this.MetricsRequest;
    }
  }
}