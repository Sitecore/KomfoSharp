// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricsRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  /// <summary>
  /// Defines the metrics request.
  /// </summary>
  public interface IMetricsRequest
  {
    /// <summary>
    /// Gets the metrics request configuration.
    /// </summary>
    /// <value>
    /// The metrics request configuration.
    /// </value>
    MetricsRequestConfiguration Configuration { get; }
  }
}