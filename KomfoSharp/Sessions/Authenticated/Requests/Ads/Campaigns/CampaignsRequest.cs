// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignsRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns
{
  using System;

  /// <summary>
  /// Represents the campaigns request.
  /// </summary>
  [Serializable]
  public class CampaignsRequest : ICampaignsRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignsRequest"/> class.
    /// </summary>
    public CampaignsRequest()
    {
      this.Configuration = new CampaignsRequestConfiguration();
    }

    /// <summary>
    /// Gets the campaigns request configuration.
    /// </summary>
    /// <value>
    /// The campaigns request configuration.
    /// </value>
    public CampaignsRequestConfiguration Configuration { get; private set; }
  }
}