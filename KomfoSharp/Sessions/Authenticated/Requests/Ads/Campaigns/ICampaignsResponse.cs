// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignsResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the response to the campaigns request.
  /// </summary>
  public interface ICampaignsResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    IEnumerable<Model.Campaign> Data { get; }
  }
}