// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignsResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Represents the response of the campaigns request.
  /// </summary>
  [Serializable]
  public class CampaignsResponse : ICampaignsResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignsResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CampaignsResponse(IEnumerable<Model.Campaign> data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<Model.Campaign> Data { get; private set; }
  }
}