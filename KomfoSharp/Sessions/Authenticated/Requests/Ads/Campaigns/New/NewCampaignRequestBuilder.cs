// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCampaignRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Model;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.New.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the new campaign request builder.
  /// </summary>
  public class NewCampaignRequestBuilder : BaseRequestBuilder, INewCampaignRequestBuilder, INewCalled, IWithPollingCalled
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCampaignRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="newCampaign">The new campaign.</param>
    public NewCampaignRequestBuilder(IConfigurationProvider configurationProvider, Campaign newCampaign)
      : base(configurationProvider)
    {
      this.NewCampaignRequest = new NewCampaignRequest();
      this.NewCampaignRequest.Configuration.Campaign = newCampaign;
    }

    /// <summary>
    /// Gets the new campaign request.
    /// </summary>
    /// <value>
    /// The new campaign request.
    /// </value>
    protected INewCampaignRequest NewCampaignRequest { get; private set; }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.NewCampaignRequest.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the new campaign request.
    /// </summary>
    /// <returns>
    /// The new campaign request.
    /// </returns>
    public INewCampaignRequest Create()
    {
      return this.NewCampaignRequest;
    }
  }
}