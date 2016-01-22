// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomAudienceFromCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  using System;

  /// <summary>
  /// Represents the response to the remove custom audience from campaign request.
  /// </summary>
  [Serializable]
  public class RemoveCustomAudienceFromCampaignResponse : IRemoveCustomAudienceFromCampaignResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveCustomAudienceFromCampaignResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public RemoveCustomAudienceFromCampaignResponse(UpdateCustomAudiencesInCampaignResponseData data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public UpdateCustomAudiencesInCampaignResponseData Data { get; private set; }
  }
}