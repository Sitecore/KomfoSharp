// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomAudienceToCampaignResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  using System;

  /// <summary>
  /// Represents the response to the add custom audiences to campaign request.
  /// </summary>
  [Serializable]
  public class AddCustomAudienceToCampaignResponse : IAddCustomAudienceToCampaignResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddCustomAudienceToCampaignResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public AddCustomAudienceToCampaignResponse(UpdateCustomAudiencesInCampaignResponseData data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public UpdateCustomAudiencesInCampaignResponseData Data { get; private set; }
  }
}