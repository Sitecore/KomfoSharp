// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the stream request configuration.
  /// </summary>
  [Serializable]
    
  public class StreamRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamRequestConfiguration"/> class.
    /// </summary>
    public StreamRequestConfiguration()
    {
      this.Fields = TweetFields.All;
    }

    /// <summary>
    /// Gets or sets the Twitter handles.
    /// </summary>
    /// <value>
    /// The Twitter handles.
    /// </value>
    public IEnumerable<string> TwitterHandles { get; set; }

    /// <summary>
    /// Gets or sets the fields.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    public TweetFields Fields { get; set; }

    /// <summary>
    /// Gets or sets the since date-time.
    /// </summary>
    /// <value>
    /// The since date-time.
    /// </value>
    public DateTime? Since { get; set; }

    /// <summary>
    /// Gets or sets the until date-time.
    /// </summary>
    /// <value>
    /// The until date-time.
    /// </value>
    public DateTime? Until { get; set; }
  }
}