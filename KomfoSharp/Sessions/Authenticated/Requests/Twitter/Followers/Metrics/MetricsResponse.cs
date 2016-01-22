// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response for the metrics request.
  /// </summary>
  [Serializable]
  public class MetricsResponse : IMetricsResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public MetricsResponse(IEnumerable<Metric> data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<Metric> Data { get; private set; }
  }
}