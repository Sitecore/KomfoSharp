// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignCustomAudiencesRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System;

  /// <summary>
  /// Represents the campaign custom audiences request configuration.
  /// </summary>
  [Serializable]
  public class CampaignCustomAudiencesRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Gets or sets the campaign identifier.
    /// </summary>
    /// <value>
    /// The campaign identifier.
    /// </value>
    public string CampaignId { get; set; }
  }
}