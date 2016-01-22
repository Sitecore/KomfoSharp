// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStreamResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response for the stream request.
  /// </summary>
  public interface IStreamResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    IEnumerable<Tweet> Data { get; }

    /// <summary>
    /// Gets the next request.
    /// </summary>
    /// <value>
    /// The next request.
    /// </value>
    IStreamRequest Next { get; }
  }
}