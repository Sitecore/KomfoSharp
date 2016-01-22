// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignCustomAudiencesResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System;
  using System.Collections.Generic;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response to the campaign custom audiences request.
  /// </summary>
  [Serializable]
  public class CampaignCustomAudiencesResponse : ICampaignCustomAudiencesResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignCustomAudiencesResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CampaignCustomAudiencesResponse(IEnumerable<CustomAudience> data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<CustomAudience> Data { get; private set; }
  }
}