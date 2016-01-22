// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFollowersCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Fluent
{
  /// <summary>
  /// Defines the call to followers request.
  /// </summary>
  public interface IFollowersCalling
  {
    /// <summary>
    /// Gets the followers requests builder.
    /// </summary>
    /// <value>
    /// The followers requests builder.
    /// </value>
    IFollowersCalled Followers { get; }
  }
}