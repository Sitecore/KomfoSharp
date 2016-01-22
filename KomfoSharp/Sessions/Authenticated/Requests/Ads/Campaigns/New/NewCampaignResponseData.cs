// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignResponseData.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;

  /// <summary>
  /// Represents the data in the response to the new campaign request.
  /// </summary>
  [Serializable]
  public class NewCampaignResponseData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignResponseData"/> class.
    /// </summary>
    /// <param name="campaignId">The campaign identifier.</param>
    public NewCampaignResponseData(string campaignId)
    {
      this.CampaignId = campaignId;
    }

    /// <summary>
    /// Gets the campaign identifier.
    /// </summary>
    /// <value>
    /// The campaign identifier.
    /// </value>
    public string CampaignId { get; private set; }
  }
}