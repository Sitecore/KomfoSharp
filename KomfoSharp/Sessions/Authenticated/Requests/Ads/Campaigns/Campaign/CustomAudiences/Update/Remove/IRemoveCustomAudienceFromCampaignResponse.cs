// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveCustomAudienceFromCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  /// <summary>
  /// Defines the response to the remove custom audience from campaign request.
  /// </summary>
  public interface IRemoveCustomAudienceFromCampaignResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    UpdateCustomAudiencesInCampaignResponseData Data { get; }
  }
}