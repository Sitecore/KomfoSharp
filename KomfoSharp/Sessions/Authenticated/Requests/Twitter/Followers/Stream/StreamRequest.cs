// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System;

  /// <summary>
  /// Represents the stream request.
  /// </summary>
  [Serializable]
  public class StreamRequest : IStreamRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamRequest"/> class.
    /// </summary>
    public StreamRequest()
    {
      this.Configuration = new StreamRequestConfiguration();
    }

    /// <summary>
    /// Gets the configuration of the request.
    /// </summary>
    /// <value>
    /// The configuration of the request.
    /// </value>
    public StreamRequestConfiguration Configuration { get; private set; }
  }
}