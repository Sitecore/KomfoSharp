// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignCustomAudiencesRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System;

  /// <summary>
  /// Represents the campaign custom audiences request.
  /// </summary>
  [Serializable]
  public class CampaignCustomAudiencesRequest : ICampaignCustomAudiencesRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignCustomAudiencesRequest"/> class.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    public CampaignCustomAudiencesRequest(string campaignId)
    {
      this.Configuration = new CampaignCustomAudiencesRequestConfiguration
      {
        CampaignId = campaignId
      };
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public CampaignCustomAudiencesRequestConfiguration Configuration { get; private set; }
  }
}