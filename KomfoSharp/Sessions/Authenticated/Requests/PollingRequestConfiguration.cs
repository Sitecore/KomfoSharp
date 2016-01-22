// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PollingConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using System;

  /// <summary>
  /// Represents the polling configuration in a request.
  /// </summary>
  [Serializable]
  public class PollingRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PollingRequestConfiguration"/> class.
    /// </summary>
    public PollingRequestConfiguration()
    {
      this.Enabled = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the polling is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if enabled; otherwise, <c>false</c>.
    /// </value>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the polling interval.
    /// </summary>
    /// <value>
    /// The polling interval.
    /// </value>
    public TimeSpan Interval { get; set; }

    /// <summary>
    /// Gets or sets the attempts count.
    /// </summary>
    /// <value>
    /// The attempts count.
    /// </value>
    public int Attempts { get; set; } 
  }
}