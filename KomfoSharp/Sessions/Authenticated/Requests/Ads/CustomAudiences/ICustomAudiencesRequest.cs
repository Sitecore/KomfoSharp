// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignCustomAudiencesRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  /// <summary>
  /// Defines the custom audiences request.
  /// </summary>
  public interface ICustomAudiencesRequest
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    CustomAudiencesRequestConfiguration Configuration { get; }
  }
}