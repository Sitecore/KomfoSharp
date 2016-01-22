// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using System;

  /// <summary>
  /// Represents the base request configuration.
  /// </summary>
  [Serializable]
  public abstract class BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRequestConfiguration"/> class.
    /// </summary>
    protected BaseRequestConfiguration()
    {
      this.Polling = new PollingRequestConfiguration();
    }

    /// <summary>
    /// Gets or sets the polling configuration.
    /// </summary>
    /// <value>
    /// The polling configuration.
    /// </value>
    public PollingRequestConfiguration Polling { get; set; }
  }
}