// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddCustomAudienceToCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  /// <summary>
  /// Defines the add custom audiences to campaign request.
  /// </summary>
  public interface IAddCustomAudienceToCampaignRequest
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
