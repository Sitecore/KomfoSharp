// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricsResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response for the metrics request.
  /// </summary>
  public interface IMetricsResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    IEnumerable<Metric> Data { get; }
  }
}