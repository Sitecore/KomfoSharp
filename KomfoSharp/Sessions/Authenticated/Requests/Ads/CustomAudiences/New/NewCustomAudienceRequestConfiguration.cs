// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the new custom audience request configuration.
  /// </summary>
  [Serializable]
  public class NewCustomAudienceRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceRequestConfiguration"/> class.
    /// </summary>
    public NewCustomAudienceRequestConfiguration()
    {
      this.CustomAudience = new CustomAudience();
    }

    /// <summary>
    /// Gets or sets the custom audience.
    /// </summary>
    /// <value>
    /// The custom audience.
    /// </value>
    public CustomAudience CustomAudience { get; set; }
  }
}