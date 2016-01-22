// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;

  /// <summary>
  /// Represents the new campaign request.
  /// </summary>
  [Serializable]
  public class NewCampaignRequest : INewCampaignRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignRequest"/> class.
    /// </summary>
    public NewCampaignRequest()
    {
      this.Configuration = new NewCampaignRequestConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public NewCampaignRequestConfiguration Configuration { get; private set; }
  }
}