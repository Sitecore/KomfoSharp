// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITwitterHandlesCalled.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent
{
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines interfaces to build request with Twitter handles specified.
  /// </summary>
  public interface ITwitterHandlesCalled :
    IFieldsCalling<IFieldsCalled, TweetFields>,
    ISinceCalling,
    IUntilCalling,
    IWithPollingCalling<IWithPollingCalled>,
    ICreateCalling<IStreamRequest>
  {
  }
}