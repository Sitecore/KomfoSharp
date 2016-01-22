// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomAudienceFromCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  using System;

  /// <summary>
  /// Represents the remove custom audience from campaign request.
  /// </summary>
  [Serializable]
  public class RemoveCustomAudienceFromCampaignRequest : IRemoveCustomAudienceFromCampaignRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveCustomAudienceFromCampaignRequest"/> class.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="updateCustomAudiencesInCampaignConfiguration">The update custom audiences in campaign configuration.</param>
    public RemoveCustomAudienceFromCampaignRequest(string campaignId, UpdateCustomAudiencesInCampaignConfiguration updateCustomAudiencesInCampaignConfiguration)
    {
      this.Configuration = new UpdateCustomAudiencesInCampaignRequestConfiguration
      {
        CampaignId = campaignId,
        CustomAudiences = updateCustomAudiencesInCampaignConfiguration
      };
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public UpdateCustomAudiencesInCampaignRequestConfiguration Configuration { get; private set; }
  }
}