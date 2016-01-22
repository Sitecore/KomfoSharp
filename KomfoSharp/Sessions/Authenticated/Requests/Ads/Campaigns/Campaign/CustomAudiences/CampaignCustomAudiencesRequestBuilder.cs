// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignCustomAudiencesRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences
{
  using System;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove;
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Campaigns.Campaign.CustomAudiences.Update.Remove.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Represents the campaign custom audiences requests builder.
  /// </summary>
  public class CampaignCustomAudiencesRequestBuilder : BaseRequestBuilder, ICampaignCustomAudiencesRequestBuilder, ICustomAudiencesCalled, Fluent.IWithPollingCalled
  {
    /// <summary>
    /// The custom audiences request
    /// </summary>
    private ICampaignCustomAudiencesRequest customAudiencesRequest;

    /// <summary>
    /// Initializes a new instance of the <see cref="CampaignCustomAudiencesRequestBuilder"/> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    public CampaignCustomAudiencesRequestBuilder(IConfigurationProvider configurationProvider, string campaignId)
      : base(configurationProvider)
    {
      this.CampaignId = campaignId;
    }

    /// <summary>
    /// Gets the campaign identifier.
    /// </summary>
    /// <value>
    /// The campaign identifier.
    /// </value>
    protected string CampaignId { get; private set; }

    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <value>
    /// The request.
    /// </value>
    protected ICampaignCustomAudiencesRequest Request
    {
      get
      {
        return this.customAudiencesRequest ?? (this.customAudiencesRequest = new CampaignCustomAudiencesRequest(this.CampaignId));
      }
    }

    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public Fluent.IWithPollingCalled WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null)
    {
      this.Request.Configuration.Polling = this.BuildPollingRequestConfiguration(pollingSetupFunc);
      return this;
    }

    /// <summary>
    /// Creates the campaign custom audiences request.
    /// </summary>
    /// <returns>
    /// The campaign custom audiences request.
    /// </returns>
    public ICampaignCustomAudiencesRequest Create()
    {
      return this.Request;
    }

    /// <summary>
    /// Adds the custom audiences configuration to add to the request.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IAddCalled Add(Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>> entitySetupFunc)
    {
      return new AddCustomAudienceToCampaignRequestBuilder(this.ConfigurationProvider, this.CampaignId, entitySetupFunc(new UpdateCustomAudiencesInCampaignBuilder()).Create());
    }

    /// <summary>
    /// Adds the custom audiences configuration to remove to the request.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>
    /// The result of the call.
    /// </returns>
    public IRemoveCalled Remove(Func<IUpdateCustomAudiencesInCampaignBuilder, ICreateCalling<UpdateCustomAudiencesInCampaignConfiguration>> entitySetupFunc)
    {
      return new RemoveCustomAudienceFromCampaignRequestBuilder(this.ConfigurationProvider, this.CampaignId, entitySetupFunc(new UpdateCustomAudiencesInCampaignBuilder()).Create());
    }
  }
}