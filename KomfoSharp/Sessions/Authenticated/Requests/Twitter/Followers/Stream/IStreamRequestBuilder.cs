// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStreamRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent;

  /// <summary>
  /// Defines the methods that are used to build the stream request.
  /// </summary>
  public interface IStreamRequestBuilder : ITwitterHandlesCalling<ITwitterHandlesCalled>
  {
     
  }
}