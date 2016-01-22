// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStreamCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream.Fluent
{
  /// <summary>
  /// Defines the call to stream requests.
  /// </summary>
  public interface IStreamCalling
  {
    /// <summary>
    /// Gets the stream requests builder.
    /// </summary>
    /// <value>
    /// The stream requests builder.
    /// </value>
    IStreamCalled Stream { get; }
  }
}