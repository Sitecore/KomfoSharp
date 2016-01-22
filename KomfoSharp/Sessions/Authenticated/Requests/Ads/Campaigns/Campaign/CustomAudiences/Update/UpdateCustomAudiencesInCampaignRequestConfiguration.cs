// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomAudiencesInCampaignRequestConfiguration.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using System;

  /// <summary>
  /// Represents the update custom audiences in campaign request configuration.
  /// </summary>
  [Serializable]
  public class UpdateCustomAudiencesInCampaignRequestConfiguration : BaseRequestConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCustomAudiencesInCampaignRequestConfiguration"/> class.
    /// </summary>
    public UpdateCustomAudiencesInCampaignRequestConfiguration()
    {
      this.CustomAudiences = new UpdateCustomAudiencesInCampaignConfiguration();
    }

    /// <summary>
    /// Gets or sets the campaign identifier.
    /// </summary>
    /// <value>
    /// The campaign identifier.
    /// </value>
    public string CampaignId { get; set; }

    /// <summary>
    /// Gets or sets the custom audiences.
    /// </summary>
    /// <value>
    /// The custom audiences.
    /// </value>
    public UpdateCustomAudiencesInCampaignConfiguration CustomAudiences { get; set; }
  }
}