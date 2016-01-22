// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomAudiencesInCampaignConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using System;

  /// <summary>
  /// Represents the update custom audiences in campaign configuration.
  /// </summary>
  [Serializable]
  public class UpdateCustomAudiencesInCampaignConfiguration
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