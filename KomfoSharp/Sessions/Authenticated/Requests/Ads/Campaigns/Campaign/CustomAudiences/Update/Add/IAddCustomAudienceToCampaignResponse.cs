// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddCustomAudienceToCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  /// <summary>
  /// Defines the add custom audiences to campaign response.
  /// </summary>
  public interface IAddCustomAudienceToCampaignResponse
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