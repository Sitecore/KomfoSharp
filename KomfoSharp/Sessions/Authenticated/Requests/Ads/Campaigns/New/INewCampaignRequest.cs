// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  /// <summary>
  /// Defines the new campaign request.
  /// </summary>
  public interface INewCampaignRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    NewCampaignRequestConfiguration Configuration { get; }
  }
}