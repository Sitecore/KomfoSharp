// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;

  /// <summary>
  /// Represents the response to the new campaign request.
  /// </summary>
  [Serializable]
  public class NewCampaignResponse : INewCampaignResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public NewCampaignResponse(NewCampaignResponseData data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public NewCampaignResponseData Data { get; private set; }
  }
}