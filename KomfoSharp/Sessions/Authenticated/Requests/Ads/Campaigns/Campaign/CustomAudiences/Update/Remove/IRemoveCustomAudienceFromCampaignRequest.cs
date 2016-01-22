// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemoveCustomAudienceFromCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  /// <summary>
  /// Defines the remove custom audiences from campaign request.
  /// </summary>
  public interface IRemoveCustomAudienceFromCampaignRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    UpdateCustomAudiencesInCampaignRequestConfiguration Configuration { get; }
  }
}