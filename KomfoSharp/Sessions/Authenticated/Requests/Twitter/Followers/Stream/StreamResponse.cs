// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response for the stream request.
  /// </summary>
  [Serializable]
  public class StreamResponse : IStreamResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="next">The next request.</param>
    public StreamResponse(IEnumerable<Tweet> data, IStreamRequest next)
    {
      this.Data = data;
      this.Next = next;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<Tweet> Data { get; private set; }

    /// <summary>
    /// Gets the next request.
    /// </summary>
    /// <value>
    /// The next request.
    /// </value>
    public IStreamRequest Next { get; private set; }
  }
}