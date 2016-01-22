// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomAudienceToCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  using System;

  /// <summary>
  /// Represents the add custom audiences to campaign request.
  /// </summary>
  [Serializable]
  public class AddCustomAudienceToCampaignRequest : IAddCustomAudienceToCampaignRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddCustomAudienceToCampaignRequest"/> class.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="updateCustomAudiencesInCampaignConfiguration">The update custom audiences in campaign configuration.</param>
    public AddCustomAudienceToCampaignRequest(string campaignId, UpdateCustomAudiencesInCampaignConfiguration updateCustomAudiencesInCampaignConfiguration)
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