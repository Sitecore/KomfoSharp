// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  /// <summary>
  /// Defines the campaign custom audiences request.
  /// </summary>
  public interface ICampaignCustomAudiencesRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CampaignCustomAudiencesRequestConfiguration Configuration { get; }
  }
}