// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExternalCampaignIdCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent
{
  /// <summary>
  /// Defines the call to specify the external campaign ID.
  /// </summary>
  public interface IExternalCampaignIdCalling
  {
    /// <summary>
    /// Specifies the external campaign ID.
    /// </summary>
    /// <param name="externalCampaignId">The external campaign identifier.</param>
    /// <returns>The result of the call.</returns>
    IExternalCampaignIdCalled ExternalCampaignId(string externalCampaignId);
  }
}