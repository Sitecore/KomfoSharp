// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStreamRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Twitter.Followers.Stream
{
  /// <summary>
  /// Defines the stream request.
  /// </summary>
  public interface IStreamRequest
  {
    /// <summary>
    /// Gets the configuration of the request.
    /// </summary>
    /// <value>
    /// The configuration of the request.
    /// </value>
    StreamRequestConfiguration Configuration { get; }
  }
}