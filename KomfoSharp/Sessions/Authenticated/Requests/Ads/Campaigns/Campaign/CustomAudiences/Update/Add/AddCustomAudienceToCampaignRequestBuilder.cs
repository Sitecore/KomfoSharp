// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomAudienceToCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the add custom audience to campaign request builder.
  /// </summary>
  public class AddCustomAudienceToCampaignRequestBuilder : BaseRequestBuilder, IAddCustomAudienceToCampaignRequestBuilder, IAddCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AddCustomAudienceToCampaignRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="updateCustomAudiencesInCampaignConfiguration">The update custom audiences in campaign configuration.</param>
    public AddCustomAudienceToCampaignRequestBuilder(IConfigurationProvider configurationProvider, string campaignId, UpdateCustomAudiencesInCampaignConfiguration updateCustomAudiencesInCampaignConfiguration)
      : base(configurationProvider)
    {
      this.Request = new AddCustomAudienceToCampaignRequest(campaignId, updateCustomAudiencesInCampaignConfiguration);
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected IAddCustomAudienceToCampaignRequest Request { get; private set; }

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
    /// Creates the add custom audiences to campaign request.
    /// </summary>
    /// <returns>
    /// The add custom audiences to campaign request.
    /// </returns>
    public IAddCustomAudienceToCampaignRequest Create()
    {
      return this.Request;
    }
  }
}