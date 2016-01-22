// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the new campaign request configuration.
  /// </summary>
  [Serializable]
  public class NewCampaignRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignRequestConfiguration"/> class.
    /// </summary>
    public NewCampaignRequestConfiguration()
    {
      this.Campaign = new Campaign();
    }

    /// <summary>
    /// Gets or sets the campaign.
    /// </summary>
    /// <value>
    /// The campaign.
    /// </value>
    public Campaign Campaign { get; set; }
  }
}