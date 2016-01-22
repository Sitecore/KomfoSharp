// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignsRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  /// <summary>
  /// Defines the campaigns request.
  /// </summary>
  public interface ICampaignsRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CampaignsRequestConfiguration Configuration { get; }
  }
}