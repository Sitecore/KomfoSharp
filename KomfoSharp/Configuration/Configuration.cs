// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration
{
  using System;

  /// <summary>
  /// Represents the KomfoSharp configuration.
  /// </summary>
  [Serializable]
  public class Configuration
  {
    /// <summary>
    /// Gets or sets the endpoints configuration.
    /// </summary>
    /// <value>
    /// The endpoints configuration.
    /// </value>
    public EndpointsConfiguration EndpointsConfiguration { get; set; }

    /// <summary>
    /// Gets or sets the polling configuration.
    /// </summary>
    /// <value>
    /// The polling configuration.
    /// </value>
    public PollingConfiguration PollingConfiguration { get; set; }
  }
}
