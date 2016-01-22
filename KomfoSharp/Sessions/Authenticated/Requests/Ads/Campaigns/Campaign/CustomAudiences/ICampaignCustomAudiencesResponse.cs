// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignCustomAudiencesResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the response to the campaign custom audiences request.
  /// </summary>
  public interface ICampaignCustomAudiencesResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    IEnumerable<CustomAudience> Data { get; }
  }
}