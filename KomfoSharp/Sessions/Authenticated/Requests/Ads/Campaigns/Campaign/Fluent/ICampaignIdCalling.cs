// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignIdCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.Fluent
{
  /// <summary>
  /// Defines the call to specify the campaign ID.
  /// </summary>
  public interface ICampaignIdCalling
  {
    /// <summary>
    /// Specifies the campaign ID.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns>The result of the call.</returns>
    ICampaignIdCalled CampaignId(string campaignId);
  }
}