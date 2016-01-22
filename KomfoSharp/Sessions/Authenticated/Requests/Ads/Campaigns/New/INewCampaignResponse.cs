// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  /// <summary>
  /// Defines the response to the new campaign request.
  /// </summary>
  public interface INewCampaignResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    NewCampaignResponseData Data { get; }
  }
}