// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetricsRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics
{
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics.Fluent;

  /// <summary>
  /// Defines the interfaces to build the metrics request.
  /// </summary>
  public interface IMetricsRequestBuilder : ITwitterHandlesCalling<ITwitterHandlesCalled>
  {
  }
}