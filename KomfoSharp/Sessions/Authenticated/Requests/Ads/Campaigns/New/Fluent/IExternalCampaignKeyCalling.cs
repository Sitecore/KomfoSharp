// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExternalCampaignKeyCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent
{
  /// <summary>
  /// Defines the call to specify the external campaign key.
  /// </summary>
  public interface IExternalCampaignKeyCalling
  {
    /// <summary>
    /// Specifies the external campaign key.
    /// </summary>
    /// <param name="externalCampaignKey">The external campaign key.</param>
    /// <returns>The result of the call.</returns>
    IExternalCampaignKeyCalled ExternalCampaignKey(string externalCampaignKey);
  }
}