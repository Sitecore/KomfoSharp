// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITwitterHandlesCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the call to specify the Twitter handles.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface ITwitterHandlesCalling<out T>
  {
    /// <summary>
    /// Specifies the Twitter handles.
    /// </summary>
    /// <param name="twitterHandles">The Twitter handles.</param>
    /// <returns>The result of the call.</returns>
    T TwitterHandles(IEnumerable<string> twitterHandles);
  }
}