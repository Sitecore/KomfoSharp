// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomAudienceFromCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the remove custom audience from campaign request builder.
  /// </summary>
  public class RemoveCustomAudienceFromCampaignRequestBuilder : BaseRequestBuilder, IRemoveCustomAudienceFromCampaignRequestBuilder, IRemoveCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveCustomAudienceFromCampaignRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="updateCustomAudiencesInCampaignConfiguration">The update custom audiences in campaign configuration.</param>
    public RemoveCustomAudienceFromCampaignRequestBuilder(IConfigurationProvider configurationProvider, string campaignId, UpdateCustomAudiencesInCampaignConfiguration updateCustomAudiencesInCampaignConfiguration)
      : base(configurationProvider)
    {
      this.Request = new RemoveCustomAudienceFromCampaignRequest(campaignId, updateCustomAudiencesInCampaignConfiguration);
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected IRemoveCustomAudienceFromCampaignRequest Request { get; private set; }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the remove custom audience from campaign request.
    /// </summary>
    /// <returns>
    /// The remove custom audience from campaign request.
    /// </returns>
    public IRemoveCustomAudienceFromCampaignRequest Create()
    {
      return this.Request;
    }
  }
}