// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics.Fluent
{
  /// <summary>
  /// Defines the call to metrics requests.
  /// </summary>
  public interface IMetricsCalling
  {
    /// <summary>
    /// Gets the metrics requests builder.
    /// </summary>
    /// <value>
    /// The metrics requests builder.
    /// </value>
    IMetricsCalled Metrics { get; }
  }
}