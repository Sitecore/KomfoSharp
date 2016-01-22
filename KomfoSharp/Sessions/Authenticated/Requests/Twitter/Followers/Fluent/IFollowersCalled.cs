// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFollowersCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent
{
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Metrics.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent;

  /// <summary>
  /// Defines interfaces to build followers requests.
  /// </summary>
  public interface IFollowersCalled : IMetricsCalling, IStreamCalling
  {
  }
}