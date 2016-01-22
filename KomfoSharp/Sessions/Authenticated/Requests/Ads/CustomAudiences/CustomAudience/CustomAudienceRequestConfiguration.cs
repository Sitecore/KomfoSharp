// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;

  /// <summary>
  /// Represents the custom audience request configuration.
  /// </summary>
  [Serializable]
  public class CustomAudienceRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Gets or sets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    public string CustomAudienceId { get; set; }
  }
}