// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the metrics request configuration.
  /// </summary>
  [Serializable]
  public class MetricsRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsRequestConfiguration"/> class.
    /// </summary>
    public MetricsRequestConfiguration()
    {
      this.Fields = MetricFields.All;
    }

    /// <summary>
    /// Gets or sets the Twitter handles.
    /// </summary>
    /// <value>
    /// The Twitter handles.
    /// </value>
    public IEnumerable<string> TwitterHandles { get; set; }

    /// <summary>
    /// Gets or sets the fields.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    public MetricFields Fields { get; set; }
  }
}