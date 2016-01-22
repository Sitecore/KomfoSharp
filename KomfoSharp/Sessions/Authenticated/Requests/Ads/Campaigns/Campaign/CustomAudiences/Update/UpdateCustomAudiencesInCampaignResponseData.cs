// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomAudiencesInCampaignResponseData.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update
{
  using System;

  /// <summary>
  /// Represents the data in the response to the update custom audiences in campaign request.
  /// </summary>
  [Serializable]
  public class UpdateCustomAudiencesInCampaignResponseData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCustomAudiencesInCampaignResponseData"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public UpdateCustomAudiencesInCampaignResponseData(string customAudienceId)
    {
      this.CustomAudienceId = customAudienceId;
    }

    /// <summary>
    /// Gets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    public string CustomAudienceId { get; private set; } 
  }
}