// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PollingRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration
{
  using System;

  /// <summary>
  /// Represents polling configuration.
  /// </summary>
  [Serializable]
  public class PollingConfiguration
  {
    /// <summary>
    /// Gets or sets the default time interval.
    /// </summary>
    /// <value>
    /// The default time interval.
    /// </value>
    public TimeSpan DefaultTimeInterval { get; set; }

    /// <summary>
    /// Gets or sets the default attempts count.
    /// </summary>
    /// <value>
    /// The default attempts count.
    /// </value>
    public int DefaultAttemptsCount { get; set; }
  }
}
