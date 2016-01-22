// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System;

  /// <summary>
  /// Represents the metrics request.
  /// </summary>
  [Serializable]
  public class MetricsRequest : IMetricsRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsRequest"/> class.
    /// </summary>
    public MetricsRequest()
    {
      this.Configuration = new MetricsRequestConfiguration();
    }

    /// <summary>
    /// Gets the metrics request configuration.
    /// </summary>
    /// <value>
    /// The metrics request configuration.
    /// </value>
    public MetricsRequestConfiguration Configuration { get; private set; }
  }
}